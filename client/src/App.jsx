import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Login_and_Reg from './components/Login_and_Register/Login_and_Reg'
import {
  Routes,
  Route,
  Link
} from "react-router-dom";

import './App.css'
import DashBoard from './components/DashBoard/Dashboard';

function App() {

  return (
    <>
    <Routes>
      <Route path='/' element={<Login_and_Reg/>}></Route>
      <Route path='/dashboard' element={<DashBoard/>}></Route>
    </Routes>
    </>
  )
}

export default App
