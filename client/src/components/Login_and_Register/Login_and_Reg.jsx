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
            <div className='d-flex justify-content-end header'>                
                <h1 className='me-3 mt-2'>That Training App</h1>
            </div>

            <div className='d-flex justify-content-center body p-5'>
                <div className='d-flex justify-content-between w-75'>
                    <div className='Reg w-50 p-5 rounded me-5'>
                        <Register triggerUpdate={triggerUpdate} />
                    </div>

                    <div className='Login w-50 p-5 rounded ms-5'>
                        <Login></Login>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Login_and_Reg