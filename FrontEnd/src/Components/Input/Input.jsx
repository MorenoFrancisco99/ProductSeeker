function Input(props) {
  return (
    <div>
      <label htmlFor={props.name}>{props.labelname}</label>
      <input type={props.type} name={props.name}id={props.id} placeholder={props.placeholder} required={props.required} pattern={props.pattern}/>
    </div>
  );
}
export default Input;
