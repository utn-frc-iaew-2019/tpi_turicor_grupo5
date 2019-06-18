//Dependencies
import React, {Component} from 'react';

class Vehiculos extends Component{

    constructor(){
        super();

        //Pedir al Back toda la lista de vehiculos
        this.state={
            vehiculos:[{
                idVehiculo:1,
                precioVenta:1200,
                disponibilidad: true},
            {
                idVehiculo:2,
                precioVenta:2000,
                disponibilidad: true},
            {
                idVehiculo:3,
                precioVenta:500,
                disponibilidad: false}]
        }
    }


    handleDisponibilidad(e){
        if(e)
        {
            return("Disponible")
        }
        else{
            return("No Disponible")
        }
    }

    render(){
        const vehiculos = this.state.vehiculos;
        return(
            <div className="Vehiculos">
                <h1>Vehiculos disponibles</h1>
                <table class="table table-striped">
                <thead class="thead-dark">
                    <tr>
                    <th scope="col">IDVehiculo</th>
                    <th scope="col">PrecioVenta</th>
                    <th scope="col">Disponibilidad</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        vehiculos.map(
                            (vehiculo, key) =>
                                <tr key={key}>
                                    <td>{vehiculo.idVehiculo}</td>
                                    <td>{vehiculo.precioVenta}</td>
                                    <td>{this.handleDisponibilidad(vehiculo.disponibilidad)}</td>
                                </tr>
                        )
                    }
                </tbody>
                </table>
            </div>
        )
    }
}

export default Vehiculos;