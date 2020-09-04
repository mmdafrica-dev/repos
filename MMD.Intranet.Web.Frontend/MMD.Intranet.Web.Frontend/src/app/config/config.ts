export let Config: any = {

    tenant: {
        key: 'extranet',
        defaultDatabase: {
            key: '1',
            value: 'MMD',
            onTrigger: {
                type: 'css',
                value: 'red'
            }
        }
    },
    appVersion: {
        version: '1.0.0.0'
    },
    count: {
        rowCount: 50
    },
    guestUser: {
        email: 'guest@mmdgphc.com',
        password: 'Abc123#'
    },
    apiUrls: {
        getHome: 'api/MainMenu/get',
        partSearch: 'api/part/search',
        accountLogin: 'api/token',
        superAdmin: {
            usersList: 'api/Account/GetUserWithRoles',
            userById: 'api/Account/user',
            roleList: 'api/Account/GetAllRoleList',
            role: 'api/Account/role',
            updateRole: 'api/Account/UpdateRole',
            deleteRole: 'api/Account/DeleteRole',
            user: 'api/Account/user',
            claims: 'api/Claims',
            register: 'api/Account/register',
            updateUser: 'api/Account/updateUserRoleClaims'
        },
        partsAndBom: {
            search: 'api/PartsAndBom/SearchPart',
            partSearchDetailOptions: 'api/PartsAndBom/PartSearchDetailOptions',
            partDetail: 'api/PartsAndBom/PartDetail',
            bomDetail: 'api/bom/BomDetail',
            partBrowse: 'api/PartsAndBom/PartBrowse',
            partNarrDetail: 'api/PartsAndBom/PartNarrDetail',
            partNarrSearch: 'api/PartsAndBom/PartNarrSearch',
            bomMultiLevel: 'api/bom/BomMultiLevel'
        },
        resetPassword: {
            changePassword: 'api/Account/ChangePassword',
            setPassword: 'api/Account/SetPassword',
        },
        partDetail: {
            bomOperationDetail: 'api/bom/bomOperationDetail',
            getPartDropDown: 'api/partDetail/GetPartDropDown',
            partBomDetail: 'api/bom/PartBomDetail',
            whereUsedDetail: 'api/bom/WhereUsedDetail',
            partAllocation: 'api/partDetail/PartAllocation',
            getTabs: 'api/MainMenu/GetTabs'
        },
        salesOrder: {
            salesOrder: 'api/salesOrder/SalesOrderLines',
            purchaseOrder: 'api/purchaseOrder/PurchaseOrderLines',
            workOrder: 'api/workOrder/WorkOrder',
            getSales: 'api/salesOrder/GetSales',
            getSalesDetail: 'api/salesOrder/GetSalesDetail',

        },
        stock: {
            stockMovements: 'api/StockMovements/StockMovement',
            binDetail: 'api/Stock/binDetail',
            lotDetail: 'api/Stock/lotDetail',
            getFxRates: 'api/StockMovements/GetFxRates'
        },
        cost: {
            costElements: 'api/cost/CostElements',
            costSet: 'api/cost/CostSet',
            standardCostHistory: 'api/cost/StandardCostHistory'
        },
        supplier: {
            supplierPrices: 'api/supplier/supplierPrices'
        },
        generic: {
            genericfile: 'api/generic/genericfile',
            genericData: 'api/generic/getdata',
            genericDropDownData: 'api/generic/getdropdowndata'
        },
        menu: {
            advancedSearchOptions: 'api/MainMenu/GetAdvancedSearchOptions',
            module: 'api/mainmenu/Get',
            mainMenu: 'api/mainmenu/GetmainMenu',
            subMenu: 'api/mainmenu/getsubmenu',
            getSearchOptions: 'api/mainmenu/GetSearchOptions',
            getFooterLink: 'api/mainmenu/GetFooterLink',
        }
    }
};