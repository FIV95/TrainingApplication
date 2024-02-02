import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Login_and_Reg from './components/Login_and_Register/Login_and_Reg'
import SessionView from './components/SessionView/Session_View'
import {
    Routes,
    Route,
    Link
} from "react-router-dom";

import './App.css'
import DashBoard from './components/DashBoard/Dashboard';
import SessionBuilder from './components/SessionBuilder/Builder';
import Construction from './components/Construction';

function App() {

    return (
        <>
            <Routes>
                <Route path='/' element={<Login_and_Reg />}></Route>
                <Route path='/dashboard' element={<DashBoard />}></Route>
                <Route path='/builder' element={<SessionBuilder />}></Route>
                <Route path='/construction' element={<Construction/>}></Route>
                <Route path='/session/:id' element={<SessionView />}></Route>
            </Routes>
        </>
    )
}

export default App
