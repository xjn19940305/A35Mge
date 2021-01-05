using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.Model.ExportModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.Api
{
    public class Init
    {
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;

        public Init(A35MgeDbContext a35MgeDbContext, IMapper mapper)
        {
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
        }

        public async Task Seeds()
        {
            await ImportMenu();
            await ImportLanType();
            await ImportTranlates();
            await ImportUser();
            await ImportRole();
            await ImportUserRole();
            await ImportRoleMenu();
        }
        #region 导入
        public async Task ImportMenu()
        {
            using (var sr = new StreamReader("../data/Menu.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<Menu>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportLanType()
        {
            using (var sr = new StreamReader("../data/LanguageType.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<LanguageType>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportTranlates()
        {
            using (var sr = new StreamReader("../data/Translate.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<Translate>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportUser()
        {
            using (var sr = new StreamReader("../data/Users.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<User>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportRole()
        {
            using (var sr = new StreamReader("../data/Role.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<Role>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportUserRole()
        {
            using (var sr = new StreamReader("../data/UserRoles.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<UserRole>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        public async Task ImportRoleMenu()
        {
            using (var sr = new StreamReader("../data/RoleMenus.json"))
            {
                var content = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<List<RoleMenu>>(content);
                foreach (var item in data)
                {
                    await a35MgeDbContext.AddAsync(item);
                }
                await a35MgeDbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region 导出
        public async Task ExportData()
        {
            await ExportMenu();
            await ExportLanType();
            await ExportTranslate();
            await ExportUser();
            await ExportRole();
            await ExportUserRole();
            await ExportRoleMenu();
        }
        private async Task ExportMenu()
        {
            var data = await a35MgeDbContext.Menu.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<MenuExport>(x)));
            using (var sw = new StreamWriter("../data/Menu.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportLanType()
        {
            var data = await a35MgeDbContext.LanguageType.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<LanExport>(x)));
            using (var sw = new StreamWriter("../data/LanguageType.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportTranslate()
        {
            var data = await a35MgeDbContext.Translate.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<TranslateExport>(x)));
            using (var sw = new StreamWriter("../data/Translate.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportUser()
        {
            var data = await a35MgeDbContext.Users.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<UserExport>(x)));
            using (var sw = new StreamWriter("../data/Users.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportRole()
        {
            var data = await a35MgeDbContext.Role.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<RoleExport>(x)));
            using (var sw = new StreamWriter("../data/Role.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportUserRole()
        {
            var data = await a35MgeDbContext.UserRoles.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<UserRoleExport>(x)));
            using (var sw = new StreamWriter("../data/UserRoles.json"))
            {
                await sw.WriteAsync(json);
            }
        }
        private async Task ExportRoleMenu()
        {
            var data = await a35MgeDbContext.RoleMenus.ToListAsync();
            var json = JsonConvert.SerializeObject(data.Select(x => mapper.Map<RoleMenuExport>(x)));
            using (var sw = new StreamWriter("../data/RoleMenus.json"))
            {
                await sw.WriteAsync(json);
            }
        }

        #endregion
    }
}
