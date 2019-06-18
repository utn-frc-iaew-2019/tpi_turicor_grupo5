//Dependencies
import React, {Component} from 'react';

class Login extends Component{
    constructor(){
    super();
    //State es un objeto que tiene propiedades o nodos que cambian dinamicamente y hacen que el Render se ejecute nuevamente.
    //Los Props son como State pero es estatico.
    this.state = {
      user: "",
      password: ""
    };

    this.handleLoginClick = this.handleLoginClick.bind(this)
    this.handleInputChanged = this.handleInputChanged.bind(this)
  };


  handleLoginClick(e){

  }


  handleInputChanged(e){
    if (e.target.id === "user"){
      this.setState({
        user: e.target.value
      })
    }
    else{
      this.setState({
        password: e.target.value
      })
    }

  }
    

    render(){
        return(
            <div className="Login">
                <h1>Login de usuario</h1>
                <p></p>
                <input id="user" placeholder="Usuario" type="string" value={this.state.user} onChange={this.handleInputChanged}></input>
                <p></p>
                <input id="password" placeholder="Contraseña" type="string" value={this.state.password} onChange={this.handleInputChanged}></input>
                <p></p>
                <button id="login" onClick={this.handleLoginClick}>Login</button>
            </div>
        )
    }
}

export default Login;