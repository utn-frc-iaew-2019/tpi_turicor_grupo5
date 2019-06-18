//Dependencies
import React, {Component} from 'react';


class Reservar extends Component{
    constructor(){
        super();

        this.state={
            idVehiculoCiudad:0,
            fechaReserva:"",
            //Pedir el precio de venta al Back
            precioVenta:50
        }

        this.handleInputChanged = this.handleInputChanged.bind(this)
        this.handleReservarClick = this.handleReservarClick.bind(this)
    };
    
    handleReservarClick(e){
    }


    handleInputChanged(e){
        if (e.target.id === "idVehiculo"){
          this.setState({
            idVehiculoCiudad: e.target.value
          })
        }
        else{
          this.setState({
            fechaReserva: e.target.value
          })
        }
    }


    render(){
        return(
            <div className="Reservar">
                <h1>Realizar reserva</h1>
                    <p>ID del Vehiculo:</p>
                    <input id="idVehiculo" type="number" value={this.state.idVehiculoCiudad} onChange={this.handleInputChanged}></input>
                    <p>Fecha de reserva:</p>
                    <input id="fechaReserva" type="string" value={this.state.fechaReserva} onChange={this.handleInputChanged}></input>
                    <p>Precio Venta: ${this.state.precioVenta}</p>
                    <button id="reservar" onClick={this.handleReservarClick}>Reservar</button>
            </div>
        )
    }
}

export default Reservar;