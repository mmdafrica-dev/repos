export interface SearchBaseResponse {

}
export interface IMenuItem {
    title: String,
    link: String,
    menuKey: String,
    searchOption: SearchBaseResponse
    isActive:boolean
}

export interface IMenu {
    title: String,
    isActive:boolean    
    items?: IMenuItem[]
}