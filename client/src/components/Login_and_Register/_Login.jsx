import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function Login() {
    const [form, setForm] = useState({
        LoginEmail: "",
        LoginPassword: "",
    });

    const [errors, setErrors] = useState({});
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);

    const onChangeHandler = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    const formHandler = async e => {
        e.preventDefault();

        console.log(
            `Email: ${form.LoginEmail}\n`,
            `Password: ${form.LoginPassword}\n`
        );

        // try to add the item, otherwise, get errors
        const addItem = await axios({
            url: "https://localhost:7116/UserBase/login",
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
            <div className='p-5 bg-white rounded'>
                <h1>Login</h1>
                <form onSubmit={formHandler} method='Post'>

                    <div className='text-start mb-3'>
                        <label htmlFor='LoginEmail'>Email:</label>
                        <input className='form-control' type='text' name='LoginEmail' value={form.LoginEmail} onChange={onChangeHandler} />
                        {
                            errors.LoginEmail && errors.LoginEmail.map((error, index) => {
                                return <p key={index} className='text-danger'>{error}</p>
                            })
                        }
                    </div>

                    <div className='text-start mb-3'>
                        <label htmlFor='LoginPassword'>Password:</label>
                        <input className='form-control' type={showPassword ? 'text' : 'password'} name='LoginPassword' value={form.LoginPassword} onChange={onChangeHandler}/>
                        <div className='form-check'>
                            <input className='form-check-input' type='checkbox' checked={showPassword} onChange={togglePasswordVisibility} id='showPasswordCheck'/>
                            <label className='form-check-label' htmlFor='showPasswordCheck'>{showPassword ? 'Hide Password' : 'Show Password'}</label>
                        </div>
                        {
                            errors.LoginPassword && errors.LoginPassword.map((error, index) => {
                                return <p key={index} className='text-danger'>{error}</p>
                            })
                        }
                    </div>


                    <button className='btn btn-primary' type='submit'>Login</button>
                </form>
            </div>
        </>
    );
}

export default Login;
