import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Login_and_Reg from './components/Login_and_Register/Login_and_Reg'
import TrainingSessionCreate from "./components/SessionBuilder/_TrainingSessionCreate";

import {
  Routes,
  Route,
  Link
} from "react-router-dom";

import './App.css'
import Success from './components/Success';

function App() {

  return (
    <>
    <Routes>
      <Route path='/' element={<Login_and_Reg/>}></Route>

      <Route path='/success' element={<Success/>}></Route>

      <Route path='/create-session' element={<TrainingSessionCreate/>}></Route>
    </Routes>
    </>
  )
}

export default App
