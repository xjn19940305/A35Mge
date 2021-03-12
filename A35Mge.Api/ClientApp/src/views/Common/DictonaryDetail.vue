<template>
  <div>
    <a-card :bordered="false">
      <a-breadcrumb>
        <a-breadcrumb-item>
          <a @click="$router.push({ path: '/' })">{{ $t('Index') }}</a>
        </a-breadcrumb-item>
        <a-breadcrumb-item>
          <a @click="$router.push({ path: '/Common/Dictonary' })">{{ $t('Dictionary_Manager') }}</a>
        </a-breadcrumb-item>
        <a-breadcrumb-item>{{ $t('Dictionary_Detail') }}</a-breadcrumb-item>
      </a-breadcrumb>
    </a-card>
    <a-card :bordered="false">
      <div class="table-page-search-wrapper">
        <a-form layout="inline">
          <a-row :gutter="48">
            <a-col :md="8" :sm="24">
              <a-form-item :label="$t('DicList_Name')">
                <a-input v-model="Name" placeholder @keydown.native.stop="handleQuery" />
              </a-form-item>
            </a-col>
            <a-col :md="8" :sm="24">
              <a-button type="primary" @click="QueryTable">{{ $t('Query') }}</a-button>
              <a-button style="margin-left: 8px" @click="ResetQuery">{{ $t('Reset') }}</a-button>
            </a-col>
          </a-row>
        </a-form>
      </div>
      <a-form layout="inline" style="margin-bottom: 10px; margin-top: 10px">
        <a-button type="primary" @click="Add()">
          {{ $t('Public_Add') }}
        </a-button>
        <a-button
          :disabled="deleteDisable"
          type="danger"
          style="margin-left: 15px"
          @click="Delete()"
          :loading="deleteLoading"
        >
          {{ $t('Public_Delete') }}</a-button
        >
      </a-form>
      <a-table
        :row-key="(record) => record.Id"
        :data-source="data"
        :loading="loading"
        :pagination="pagination"
        :row-selection="rowSelection"
        @change="handleTableChange"
      >
        <a-table-column key="Id" data-index="Id" :title="$t('Id')" />
        <a-table-column key="DicTypeName" data-index="DicTypeName" :title="$t('Dic_Name')">
          <template slot-scope="text, record">
            <span>
              {{ record.DicType.Name }}
            </span>
          </template>
        </a-table-column>
        <a-table-column key="Name" data-index="Name" :title="$t('DicList_Name')" />
        <a-table-column key="Sort" data-index="Sort" :title="$t('Sort')" />
        <a-table-column key="CreateDate" data-index="CreateDate" :title="$t('CreateDate')">
          <template slot-scope="text, record">
            <span>
              {{ new Date(record.CreateDate).toLocaleString() }}
            </span>
          </template>
        </a-table-column>
        <a-table-column key="action" :title="$t('Action')">
          <template slot-scope="text, record">
            <span>
              <a @click="Save(record.Id)">{{ $t('Public_Update') }}</a>
              <a-divider type="vertical" />
            </span>
          </template>
        </a-table-column>
      </a-table>
      <a-modal
        :title="dialogTitle"
        v-model="show"
        :centered="true"
        :maskClosable="false"
        :width="800"
        @cancel="handleCancel"
        @ok="onSubmit"
      >
        <a-form-model ref="ruleForm" :model="form" :rules="rules" :label-col="labelCol" :wrapper-col="wrapperCol">
          <a-form-model-item ref="Name" :label="$t('DicList_Name')" prop="Name" @keydown.native.stop="handleSubmit">
            <a-input v-model="form.Name" />
          </a-form-model-item>
          <a-form-model-item ref="Sort" :label="$t('Sort')" prop="Sort" @keydown.native.stop="handleSubmit">
            <a-input v-model="form.Sort" />
          </a-form-model-item>
        </a-form-model>
      </a-modal>
    </a-card>
  </div>
</template>

<script>
import DicListApi from '@/api/diclist'
export default {
  data () {
    return {
      DicTypeId: 0,
      Name: '',
      data: [],
      loading: false,
      show: false,
      deleteLoading: false,
      dialogTitle: '',
      selectedRowKeys: [],
      deleteDisable: true,
      pagination: { defaultCurrent: 1, defaultPageSize: 20, showSizeChanger: true, pageSizeOptions: ['50', '100', '500'] },
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      form: {
        Name: '',
        DicTypeId: 0,
        Sort: null
      },
      rules: {
        Name: [
          { required: true, message: this.$t('REQUIRE'), trigger: 'blur' }
        ]
      }
    }
  },
  mounted () {
    this.Init()
    this.QueryTable()
  },
  watch: {
  },
  computed: {
    rowSelection: function () {
      return {
        selectedRowKeys: this.selectedRowKeys,
        onChange: this.onSelectChange
      }
    }
  },
  methods: {
    handleSubmit (e) {
      var eCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode
      if (eCode === 13) {
        // 调用对应的方法
        this.onSubmit()
      }
    },
    Init () {
      this.DicTypeId = this.$route.query.Id
      this.form.DicTypeId = this.DicTypeId
    },
    ResetQuery () {
      this.Title = ''
    },
    // 回车方法
    handleQuery (e) {
      var eCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode
      if (eCode === 13) {
        // 调用对应的方法
        this.QueryTable()
      }
    },
    // 多选的方法
    onSelectChange (selectedRowKeys) {
      selectedRowKeys.length > 0 ? this.deleteDisable = false : this.deleteDisable = true
      this.selectedRowKeys = selectedRowKeys
    },
    handleTableChange (pagination, filters, sorter) {
      console.log(pagination)
      const pager = { ...this.pagination }
      pager.current = pagination.current
      pager.defaultPageSize = pagination.pageSize
      this.pagination = pager
      this.fetch({
        pageSize: pagination.pageSize,
        page: pagination.current,
        Name: this.Name || '',
        ...filters
      })
    },
    QueryTable () {
      var current = this.pagination.current || this.pagination.defaultCurrent
      this.fetch({
        pageSize: this.pagination.defaultPageSize,
        page: current,
        Name: this.Name || '',
        DicTypeId: this.DicTypeId || 0
      })
    },
    async fetch (params = {}) {
      console.log('params:', params)
      const pagination = { ...this.pagination }
      params.page = params.page || pagination.defaultCurrent
      params.pageSize = params.pageSize || pagination.defaultPageSize
      this.loading = true
      var res = await DicListApi.getList(params)
      pagination.total = Number(res.totalElements)
      this.loading = false
      var result = res.Data || []
      this.data = result
      this.pagination = pagination
    },
    // 新增
    Add () {
      this.dialogTitle = this.$t('Public_Add')
      this.op = 'add'
      this.show = true
    },
    async Save (id) {
      this.dialogTitle = this.$t('Public_Update')
      this.op = 'update'
      this.show = true
      var data = await DicListApi.Get(id)
      this.form = data
    },
    async Delete () {
      var data = this.selectedRowKeys
      this.deleteLoading = true
      await DicListApi.Delete(data)
      this.$notification.success({
        message: this.$t('Notiication'),
        description: this.$t('DeleteOk')
      })
      this.selectedRowKeys = []
      this.QueryTable()
      this.deleteLoading = false
    },
    // 取消
    handleCancel () {
      this.show = false
      this.resetForm()
    },
    onSubmit () {
      this.$refs.ruleForm.validate(async valid => {
        if (valid) {
          console.log(this.form)
          if (this.op === 'add') {
            DicListApi.Add(this.form).then((res) => {
              this.$notification.success({
                message: this.$t('Notiication'),
                description: this.$t('SaveOk')
              })
              this.show = false
              this.resetForm()
              this.QueryTable()
            })
          } else {
            DicListApi.Update(this.form).then((res) => {
              this.$notification.success({
                message: this.$t('Notiication'),
                description: this.$t('SaveOk')
              })
              this.show = false
              this.resetForm()
              this.QueryTable()
            })
          }
        }
      })
    },
    resetForm () {
      this.$refs.ruleForm.resetFields()
      this.form = {
        Name: '',
        DicTypeId: this.DicTypeId,
        Sort: null
      }
    }
  }
}
</script>
<style>
img {
  width: 200px;
  height: 100px;
}
</style>
