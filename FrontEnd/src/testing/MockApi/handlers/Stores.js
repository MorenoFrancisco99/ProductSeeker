import business from "../data/business_mock.json"

export function GetBusiness(){
    return Promise.resolve(business)
}

export function PostBusiness(newBusiness){
    return Promise.resolve(newBusiness)
}