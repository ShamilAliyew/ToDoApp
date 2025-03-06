import React, { useState } from 'react';
import '../styles/SignUp.css';

function SignUp() {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        username: '',
        password: '',
        email: ''
    });

    // Form məlumatlarını idarə etmək üçün handler funksiyası
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    // Form göndərildikdə məlumatları API-yə göndərmək üçün
    const handleSubmit = async (e) => {
        e.preventDefault();

        // API URL-sini buraya əlavə edin (məsələn:)
        const apiUrl = 'http://localhost:7116/api/User/add';

        try {
            const response = await fetch(apiUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData), // Form məlumatlarını JSON şəklində göndəririk
            }); 

            if (response.ok) {
                // Məlumat müvəffəqiyyətlə göndərildi
                const responseData = await response.json();
                alert('User registered successfully!');
                console.log(responseData);
            } else {
                // API tərəfindən bir xəta baş verdi
                const errorData = await response.json();
                alert('Registration failed: ' + errorData.message);
                console.log(errorData);
            }
        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while registering.');
        }
    };

    return (
        <div>
            <h1>Sign Up</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="firstName">First Name</label>
                    <input
                        type="text"
                        id="firstName"
                        name="firstName"
                        value={formData.firstName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="lastName">Last Name</label>
                    <input
                        type="text"
                        id="lastName"
                        name="lastName"
                        value={formData.lastName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="username">Username</label>
                    <input
                        type="text"
                        id="username"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="email">Email</label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <button type="submit">Sign Up</button>
                </div>
            </form>
        </div>
    );
}

export default SignUp;
