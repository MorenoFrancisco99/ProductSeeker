import NewStoreForm from "../../Components/NewStoreForm/NewStoreForm"
import ScrolleableListSingleBttn from "../../Components/ScrolleableLists/ScrolleableListSingleButton/ScrolleableListSingleBttn"

function MisTiendas(){

    return(
        <div>
            <ScrolleableListSingleBttn />

            <NewStoreForm isToggleable={true}/>
        </div>
)
}
export default MisTiendas