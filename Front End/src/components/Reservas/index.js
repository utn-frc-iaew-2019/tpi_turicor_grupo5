//Dependencies
import React, {Component} from 'react';

class Reservas extends Component{

    //En reservas me tienen que pasar desde el Back el JSON con todas las reservas
    constructor(){
        super();

        this.state={
            reservas:[{
                codigoReserva:1,
                fechaReserva:"09-10-11",
                idCliente:50,
                costo:1000,
                precioVenta:1200},
            {
                codigoReserva:2,
                fechaReserva:"12-12-12",
                idCliente:51,
                costo:1500,
                precioVenta:1800},
            {
                codigoReserva:3,
                fechaReserva:"15-04-18",
                idCliente:55,
                costo:2000,
                precioVenta:2400}],
            codigoReserva:""
        }

        this.handleInputChanged = this.handleInputChanged.bind(this)
        this.handleCancelarClick = this.handleCancelarClick.bind(this)
    }
    


    
    handleInputChanged(e){
        this.setState({
            codigoReserva: e.target.value
        })
    }

    //Mandar el ID al Back
    handleCancelarClick(){

    }


    render(){
        const reservas = this.state.reservas;
        return(
            <div className="Reservas">
                <h1>Reservas realizadas</h1>
                <table class="table table-striped">
                <thead class="thead-dark">
                    <tr>
                    <th scope="col">CodigoReserva</th>
                    <th scope="col">FechaReserva</th>
                    <th scope="col">IDCliente</th>
                    <th scope="col">Costo</th>
                    <th scope="col">PrecioVenta</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        reservas.map(
                            (reserva, key) =>
                                <tr key={key}>
                                    <td>{reserva.codigoReserva}</td>
                                    <td>{reserva.fechaReserva}</td>
                                    <td>{reserva.idCliente}</td>
                                    <td>{reserva.costo}</td>
                                    <td>{reserva.precioVenta}</td>
                                </tr>
                        )
                    }
                </tbody>
                </table>

                <h1>Cancelar Reserva</h1>
                <p></p>
                <input id="codigoReserva" type="number" placeholder="Codigo Reserva" value={this.state.codigoReserva} onChange={this.handleInputChanged}></input>
                <button id="cancelar" onClick={this.handleCancelarClick}>Cancelar reserva</button>
            </div>
        )
    }
}

export default Reservas;