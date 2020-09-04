export interface SearchBaseResponse {

}

export interface IHome {
    title: String,
    isActive: boolean
    items?: IHomeItem[]
}

export interface IHomeItem {
    title: String,
    link: String,
    menuKey: String,
    searchOption: any
    isActive: boolean
}