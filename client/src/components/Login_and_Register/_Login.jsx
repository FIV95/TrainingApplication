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
        email: "",
        password: ""
    });

    const onChangeHandler = e => {
        setForm({...form, [e.target.name]: e.target.value})
    }

    const formHandler = async e => {
        e.preventDefault();

        console.log(
            `Email: ${form.email}\n`,
            `Password: ${form.password}\n`
        );

            const addItem = await axios({
                url: "https://localhost:7116/UserBase/login",
                method: "post",
                data: form,
                contentType: "application/json"
            }).then( res =>{
    
                navigate("/success")

            }).catch (err => {
                        if (err.response.data)
                        {
                            console.log(err.response.data);
                            setErrors(err.response.data) 
                        }
            })
    }

    return (
        <>
            <div>
                <h1>Login</h1>
                <form onSubmit={formHandler} method='Post'>
                    <div className='text-start mb-3'>
                        <label htmlFor='email'>Email:</label>
                        <input className='form-control' type='text' name='email' value={form.email} onChange={onChangeHandler}></input>
                        {
                            errors.Email && errors.Email.map((error, index) => {
                                return <p key={index} className='text-danger'>{error}</p>
                            })
                        }

                    </div>
                    <div className='text-start mb-3'>
                        <label htmlFor='password'>Password</label>
                        <input className='form-control' type='text' name='password' value={form.password} onChange={onChangeHandler}></input>
                        {
                            errors.Password && errors.Password.map((error, index) => {
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