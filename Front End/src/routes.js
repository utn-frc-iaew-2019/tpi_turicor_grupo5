//Dependencies
import React from 'react';
import {Route, Switch} from 'react-router-dom';

//Components
import App from './components/App';
import Login from './components/Login';
import Reservar from './components/Reservar';
import Reserva from './components/Reserva';
import Reservas from './components/Reservas';
import Vehiculos from './components/Vehiculos';
import Home from './components/Home';

const AppRoutes = () =>
<App>
    <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/login" component={Login} />
        <Route exact path="/reservar" component={Reservar} />
        <Route exact path="/reserva" component={Reserva} />
        <Route exact path="/reservas" component={Reservas} />
        <Route exact path="/vehiculos" component={Vehiculos} />
    </Switch>
</App>

export default AppRoutes;