using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Infrastructure;
using A35Mge.Model.common;
using A35Mge.Model.Permission.User;
using A35Mge.Service.Interface;
using A35Mge.Service.Permission;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly JwtService jwtService;

        public UserService(
            IMapper mapper,
            A35MgeDbContext a35MgeDbContext,
            JwtService jwtService
            )
        {
            this.mapper = mapper;
            this.a35MgeDbContext = a35MgeDbContext;
            this.jwtService = jwtService;
        }
        public async Task Add(UserDTO dto)
        {
            var entity = mapper.Map<User>(dto);
            if (await a35MgeDbContext.Users.FirstOrDefaultAsync(x => x.Id == entity.Id) != null)
                throw new Exception("账号已存在!");
            entity.Password = "123456".Get32Md5();
            await a35MgeDbContext.AddAsync(entity);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task Update(UserDTO dto)
        {
            var entity = await a35MgeDbContext.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
            var data = mapper.Map(dto, entity);
            data.ModifyDate = DateTime.Now;
            a35MgeDbContext.Update(data);
            await a35MgeDbContext.SaveChangesAsync();
        }
        public async Task Delete(int[] ids)
        {
            var data = await a35MgeDbContext.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<UserDTO> Get(int Id)
        {
            var data = await a35MgeDbContext.Users.FirstOrDefaultAsync(X => X.Id == Id);
            return mapper.Map<UserDTO>(data);
        }

        public async Task<PagedViewModel> GetList(UserRequest request)
        {
            var model = new PagedViewModel();
            var data = a35MgeDbContext
                .Users
                .OrderByDescending(x=>x.CreateDate)
                .AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Account))
                data = data.Where(x => x.Account.Contains(request.Account));
            if (!string.IsNullOrWhiteSpace(request.Email))
                data = data.Where(x => x.Email.Contains(request.Email));
            if (!string.IsNullOrWhiteSpace(request.NickName))
                data = data.Where(x => x.NickName.Contains(request.NickName));
            if (!string.IsNullOrWhiteSpace(request.Phone))
                data = data.Where(x => x.Phone.Contains(request.Phone));
            model.totalElements = data.Count();
            data = data.Skip((request.page - 1) * request.pageSize).Take(request.pageSize);
            model.Data = await data.Select(x => mapper.Map<UserDTO>(x)).ToListAsync();
            return model;
        }

        public async Task<int[]> GetRoleIds(int UserId)
        {
            var data = await a35MgeDbContext.UserRoles.Where(x => x.UserId == UserId).Select(x => x.RoleId).ToListAsync();
            return data.ToArray();
        }

        public async Task SaveUserRole(UserRoleRequest request)
        {
            var data = await a35MgeDbContext.UserRoles.Where(x => x.UserId == request.UserId && request.RoleIds.Contains(x.RoleId)).ToListAsync();
            a35MgeDbContext.RemoveRange(data);
            foreach (var item in request.RoleIds)
            {
                await a35MgeDbContext.UserRoles.AddAsync(new UserRole { RoleId = item, UserId = request.UserId });
            }
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task ResetPwd(int UserId)
        {
            var user = await a35MgeDbContext.Users.FirstOrDefaultAsync(x => x.Id == UserId);
            user.Password = "123456".Get32Md5();
            a35MgeDbContext.Update(user);
            await a35MgeDbContext.SaveChangesAsync();
        }

        public async Task<string> Login(LoginDTO dto)
        {
            var pwd = dto.Password.Get32Md5();
            var entity = await a35MgeDbContext.Users.FirstOrDefaultAsync(x => x.Account == dto.Account && x.Password == pwd);
            if (entity == null)
                throw new Exception("账号或密码错误!");
            return jwtService.GetToken(entity.Id.ToString());
        }

        public async Task<UserDTO> GetUserInfo(string Token)
        {
            var TokenInfo = jwtService.SerializeJwt(Token.Replace("Bearer ", string.Empty));
            var id = TokenInfo.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var entity = await a35MgeDbContext.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id.Value);
            if (entity == null)
                throw new Exception("用户信息不存在!");
            var data = mapper.Map<UserDTO>(entity);
            var list = await (from p in a35MgeDbContext.UserRoles
                              join pp in a35MgeDbContext.RoleMenus
                              on p.RoleId equals pp.RoleId
                              where p.UserId == entity.Id
                              select pp
                     ).ToListAsync();
            data.MenuIds = list.Select(x => x.MenuId).Distinct().ToArray();
            return data;
        }
    }
}
