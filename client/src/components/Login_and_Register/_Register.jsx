import { useState } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import { useNavigate } from "react-router-dom";

function Register(props) {
	const [form, setForm] = useState({
		firstName: "",
		lastName: "",
		UserType: "Client",
		email: "",
		password: "",
		passwordConfirm: "",
	});

	const [errors, setErrors] = useState({});
	const navigate = useNavigate();
	const [registrationSuccess, setRegistrationSuccess] = useState(false);
	const [showPassword, setShowPassword] = useState(false);

	const onChangeHandler = (e) => {
		setForm({ ...form, [e.target.name]: e.target.value });
	};

	// This empties the form after you submit
	const formReset = () => {
		setForm({
			firstName: "",
			lastName: "",
			UserType: "Client",
			email: "",
			password: "",
			passwordConfirm: "",
		});
	};

	const togglePasswordVisibility = () => {
		setShowPassword(!showPassword);
	};

	const formHandler = async (e) => {
		e.preventDefault();

		console.log(
			`First Name: ${form.firstName}\n`,
			`Last Name: ${form.lastName}\n`,
			`User Type: ${form.UserType}\n`,
			`Email: ${form.email}\n`,
			`Password: ${form.password}\n`,
			`Confirm Password: ${form.passwordConfirm}\n`
		);

		// try to add the item, otherwise, get errors
		const addItem = await axios({
			url: "http://localhost:5252/UserBase",
			method: "post",
			data: form,
			contentType: "application/json",
		})
			.then((res) => {
				setRegistrationSuccess(true);
			})
			.catch((err) => {
				if (err.response.data) {
					console.log(err.response.data);
					setErrors(err.response.data);
				}
			});
	};

	return (
		<>
			<div>
				{registrationSuccess ? (
					<div class="alert alert-success mt-5" role="alert">
						Registration Successful! Please Log in
					</div>
				) : (
					<div>
						<h1>Register</h1>
						<form onSubmit={formHandler} method="Post">
							<div className="text-start mb-3">
								<label htmlFor="firstName">First Name:</label>
								<input
									className="form-control"
									type="text"
									name="firstName"
									value={form.firstName}
									onChange={onChangeHandler}
								/>
								{errors.FirstName &&
									errors.FirstName.map((error, index) => {
										return (
											<p key={index} className="text-danger">
												{error}
											</p>
										);
									})}
							</div>

							<div className="text-start mb-3">
								<label htmlFor="lastName">Last Name:</label>
								<input
									className="form-control"
									type="text"
									name="lastName"
									value={form.lastName}
									onChange={onChangeHandler}
								/>
								{errors.LastName &&
									errors.LastName.map((error, index) => {
										return (
											<p key={index} className="text-danger">
												{error}
											</p>
										);
									})}
							</div>

							<div className="text-start mb-3">
								<label htmlFor="email">Email:</label>
								<input
									className="form-control"
									type="text"
									name="email"
									value={form.email}
									onChange={onChangeHandler}
								/>
								{errors.Email &&
									errors.Email.map((error, index) => {
										return (
											<p key={index} className="text-danger">
												{error}
											</p>
										);
									})}
							</div>

							<div className="text-start mb-3">
								<label htmlFor="password">Password:</label>
								<input
									className="form-control"
									type={showPassword ? "text" : "password"}
									name="password"
									value={form.password}
									onChange={onChangeHandler}
								></input>

								{errors.Password &&
									errors.Password.map((error, index) => {
										return (
											<p key={index} className="text-danger">
												{error}
											</p>
										);
									})}
							</div>

							<div className="text-start mb-3">
								<label htmlFor="passwordConfirm">Confirm Password:</label>
								<input
									className="form-control"
									type={showPassword ? "text" : "password"}
									name="passwordConfirm"
									value={form.passwordConfirm}
									onChange={onChangeHandler}
								></input>
								{errors.PasswordConfirm &&
									errors.PasswordConfirm.map((error, index) => {
										return (
											<p key={index} className="text-danger">
												{error}
											</p>
										);
									})}
							</div>
							<div className="form-check">
								<input
									className="form-check-input"
									type="checkbox"
									value={showPassword}
									onChange={togglePasswordVisibility}
									id="showPasswordCheck"
								/>
								<label
									className="form-check-label"
									style={{ marginLeft: "-100px" }}
									htmlFor="showPasswordCheck"
								>
									{showPassword ? "Hide Passwords" : "Show Passwords"}
								</label>
							</div>

							<div className="mb-3">
								<label>Are you registering as:</label>

								<div className="d-flex justify-content-center pt-3">
									<div className="me-3">
										<input
											className="form-check-input"
											type="radio"
											name="UserType"
											value="Coach"
											// checked={form.UserType === 'Coach'}
											onChange={onChangeHandler}
										/>
										<label>Coach</label>
									</div>

									<div>
										<input
											className="form-check-input"
											type="radio"
											name="UserType"
											value="Client"
											checked={true}
											onChange={onChangeHandler}
										/>
										<label>Client</label>
									</div>
								</div>
							</div>

							<button className="btn btn-primary" type="submit">
								Register
							</button>
						</form>
					</div>
				)}
			</div>
		</>
	);
}

export default Register;
