import react from "react";

const Item = props => (
  <div className="col-md-4">
    <img src="https://dummyimage.com/303x303/000/fff" class="img-responsive" />
    <div>Name: {props.name}</div>
    <div>Price: ${props.price}</div>
  </div>
);

export default Item;
