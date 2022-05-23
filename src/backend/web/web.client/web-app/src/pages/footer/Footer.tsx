import * as React from "react";
import { NavLink } from "react-router-dom";
import "./Footer.css";

class Footer extends React.Component {
  

  public render() {
    const activeStyle = {
      color: "#4282ad",
      textDecoration: "underline"
    };

    const defaultStyle = {
      textDecoration: "none",
      color: "rgb(32, 32, 34)"
    };

    return (
      <div
        style={{
          boxSizing: "border-box",
          padding: 10,
          borderTop: "1px solid lightgray",
          height: 100,
          backgroundColor: "#f1f1f1",
          justifyContent: "space-around",
          display: "flex"
        }}
      >
        <div>
          <div
            style={{ color: "#504F5A", fontWeight: "bold", marginBottom: 10 }}
          >
            Buy
          </div>
          <NavLink
            to={"/payment"}
            style={(isActive) => {
              return isActive ? activeStyle : defaultStyle;
            }}
          >
            <div className="footerItem">Terms of payment</div>
          </NavLink>
          <NavLink
            to={"/delivery"}
            style={(isActive) => {
              return isActive ? activeStyle : defaultStyle;
            }}
          >
            <div className="footerItem">Delivery</div>
          </NavLink>
        </div>
        <div>
          <div
            style={{ color: "#504F5A", fontWeight: "bold", marginBottom: 10 }}
          >
            About us
          </div>
          <NavLink
            to={"/info"}
            style={(isActive) => {
              return isActive ? activeStyle : defaultStyle;
            }}
          >
            <div className="footerItem">Company Info</div>
          </NavLink>
        </div>
        <div>
          <div
            style={{ color: "#504F5A", fontWeight: "bold", marginBottom: 10 }}
          >
            Social Media
          </div>
          <a
            href="http://www.facebook.com"
            target="blank"
            style= {defaultStyle}
          >
            <div className="footerItem">Facebook</div>
          </a>
        </div>
      </div>
    );
  }
}

export default Footer;
