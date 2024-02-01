import { useState } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios'
import {
    useNavigate
} from "react-router-dom";

function Login() {
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);

    const [form, setForm] = useState({
        LoginEmail: "",
        LoginPassword: ""
    });

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    }

    const onChangeHandler = e => {
        setForm({ ...form, [e.target.name]: e.target.value })
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
        }).then(res => {

            const userType = res.data.user.userType;
            const userId = res.data.user.userId;

            sessionStorage.setItem('UserType', userType);
            sessionStorage.setItem('UserId', userId);
            sessionStorage.setItem('firstName', res.data.user.firstName);

            navigate("/dashboard")

        }).catch(err => {
            if (err.response && err.response.data) {
                console.log(err.response.data.errors);
                if (err.response.data.errors) {
                    setErrors(err.response.data.errors);
                } else if (err.response.data.LoginEmail) {
                    setErrors({ LoginEmail: err.response.data.LoginEmail });
                }
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
                        <input className='form-control' type={showPassword ? 'text' : 'password'} name='LoginPassword' value={form.LoginPassword} onChange={onChangeHandler}></input>
                        <div className="form-check">
                            <input className="form-check-input" type="checkbox" value={showPassword} onChange={togglePasswordVisibility} id="showPasswordCheck" />
                            <label className="form-check-label" htmlFor="showPasswordCheck">
                                {showPassword ? 'Hide Password' : 'Show Password'}
                            </label>
                        </div>
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
