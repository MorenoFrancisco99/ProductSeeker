import { useState } from "react"
function SearchBar ( {onSearch}) {
    const [Term, setTerm] = useState("")
    
    const HandleChange = (e) => {
        setTerm(e.target.value)
        onSearch(e.target.value)
    }
    return (
        <>
        <input type="text" name="" id="" value={Term} onChange={HandleChange}/>
        </>
    )
}
export default SearchBar