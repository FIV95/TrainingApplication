import { useState } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios'
import {
    useNavigate
} from "react-router-dom";

function Login() {
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

    const [form, setForm] = useState({
        LoginEmail: "",
        LoginPassword: ""
    });

    const onChangeHandler = e => {
        setForm({...form, [e.target.name]: e.target.value})
    }

    const formHandler = async e => {
        e.preventDefault();

        console.log(
            `Email: ${form.LoginEmail}\n`,
            `Password: ${form.LoginPassword}\n`
        );

            const addItem = await axios({
                url: "http://localhost:5252/UserBase/login",
                method: "post",
                data: form,
                contentType: "application/json"
            }).then( res =>{

                navigate("/dashboard")

            }).catch (err => {
                        if (err.response.data)
                        {
                            console.log(err.response.data.errors);
                            setErrors(err.response.data.errors)
                        }
            })
    }

    return (
        <>
            <div>
                <h1>Login</h1>
                <form onSubmit={formHandler} method='Post'>
                    <div className='text-start mb-3'>
                        <label htmlFor='LogEmail'>Email:</label>
                        <input className='form-control' type='text' name='LoginEmail' value={form.LoginEmail} onChange={onChangeHandler}></input>
                        {
                            errors.LoginEmail && errors.LoginEmail.map((error, index) => {
                                return <p key={index} className='text-danger'>{error}</p>
                            })
                        }

                    </div>
                    <div className='text-start mb-3'>
                        <label htmlFor='LoginPassword'>Password</label>
                        <input className='form-control' type='text' name='LoginPassword' value={form.LoginPassword} onChange={onChangeHandler}></input>
                        {
                            errors.LoginPassword && errors.LoginPassword.map((error, index) => {
                                return <p key={index} className='text-danger'>{error}</p>
                            })
                        }
                    </div>

                    <button className='btn btn-primary'>Login</button>
                </form>
            </div>
        </>
    )
}

export default Login
