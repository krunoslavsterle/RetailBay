import React from "react";
import Item from "./Item";

const Home = props => {
  return (
    <div>
      {props.products.map(product => (
        <Item
          key={product.id}
          name={product.name}
          price={product.productPrice.price}
        ></Item>
      ))}
    </div>
  );
};

export default Home;
