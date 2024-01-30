import { useState } from 'react'
import Register from './_Register'
import Login from './_Login'
import 'bootstrap/dist/css/bootstrap.min.css';

function Login_and_Reg() {
    const [update, setUpdate] = useState(false);

    const triggerUpdate = () => {
        setUpdate(!update);
    }

    return (
        <>
            <div className='d-flex justify-content-between w-50'>
                <Register triggerUpdate = {triggerUpdate}/>
                <Login></Login>
            </div>
        </>
    )
}

export default Login_and_Reg