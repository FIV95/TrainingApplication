import { useState } from 'react'
import axios from 'axios'
import 'bootstrap/dist/css/bootstrap.min.css';
import {
    Routes,
    Route,
    Link,
    useNavigate
} from "react-router-dom";

function Register() {
    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        UserType: "",
        email: "",
        password: "",
        confirmPassword: ""
    });
        
    const [errors, setErrors] = useState(null);
    const navigate = useNavigate();

    const onChangeHandler = e => {
        setForm({...form, [e.target.name]: e.target.value})
    }

    // This empties the form after you submit
    const formReset = () => {
        setForm({
            firstName: "",
            lastName: "",
            UserType: "",
            email: "",
            password: "",
            confirmPassword: ""
        });
    };

    const formHandler = async e => {
        e.preventDefault();

        console.log(
            `First Name: ${form.firstName}\n`,
            `Last Name: ${form.lastName}\n`,
            `User Type: ${form.UserType}\n`,
            `Email: ${form.email}\n`,
            `Password: ${form.password}\n`,
            `Confirm Password: ${form.confirmPassword}\n`
        );

        // try to add the item, otherwise, get errors
        try {
            const addItem = await axios({
                url: "https://localhost:7116/UserBase",
                method: "post",
                data: form,
                contentType: "application/json"
            });
        
            // Triggers an update to the page whenever a task is added to the list.
            props.triggerUpdate()
            // Clears the form
            formReset();
            // Resets errors to null in case there had been some
            setErrors(null);

            useNavigate("/success")

            } catch (err) {
                // Only Name is capable of getting an error
                // Pull one layer back if you have multiple errors to watch out for
                // console.log(err.response);
                setErrors(err.response.data.errors);
                console.log(errors)
            }
    };

    return (
        <>
            <div>
                <h1>Register</h1>
                <form onSubmit={formHandler}>
                    {errors ? <span className="text-danger">{errors}</span> : ""}

                    <div className='text-start mb-3'>
                        <label htmlFor='firstName'>First Name:</label>
                        <input className='form-control' type='text' name='firstName' value={form.firstName} onChange={onChangeHandler} />
                    </div>
                        
                    <div className='text-start mb-3'>
                        <label htmlFor='lastName'>Last Name:</label>
                        <input className='form-control' type='text' name='lastName' value={form.lastName} onChange={onChangeHandler}/>
                    </div>

                    <div className='text-start mb-3'>
                        <label htmlFor='email'>Email:</label>
                        <input className='form-control' type='text' name='email' value={form.email} onChange={onChangeHandler}/>
                    </div>

                    <div className='text-start mb-3'>
                        <label htmlFor='password'>Password:</label>
                        <input className='form-control' type='text' name='password' value={form.password} onChange={onChangeHandler}/>
                    </div>

                    <div className='text-start mb-3'>
                        <label htmlFor='confirmPassword'>Confirm Password:</label>
                        <input className='form-control' type='text' name='confirmPassword' value={form.confirmPassword} onChange={onChangeHandler}/>
                    </div>

                    <div className='mb-3'>
                        <label>Are you registering as:</label>

                        <div className='d-flex justify-content-center pt-3'>
                            <div className='me-3'>
                                <input className='form-check-input'
                                    type='radio'
                                    name='UserType'
                                    value='Coach'
                                    checked={form.UserType === 'Coach'}
                                    onChange={onChangeHandler}
                                    />
                                <label>Coach</label>
                            </div>

                            <div>
                                <input className='form-check-input'
                                    type='radio'
                                    name='UserType'
                                    value='Client'
                                    checked={form.UserType === 'Client'}
                                    onChange={onChangeHandler}
                                    />
                                <label>Client</label>
                            </div>
                        </div>
                    </div>

                    <button className='btn btn-primary' type='submit'>Register</button>
                </form>
            </div>

        </>
    )
}

export default Register