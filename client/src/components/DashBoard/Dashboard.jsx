import { useState, useEffect } from 'react'
// import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { Button, ListGroup, Container, Row, Col } from 'react-bootstrap';
import axios from 'axios'
import {
    useNavigate
} from "react-router-dom";
import CoachDashboard from './_CoachDashboard';
import ClientDashboard from './_ClientDashboard';

function DashBoard() {
    const [userType, setUserType] = useState(sessionStorage.getItem('UserType'));

    useEffect(() => {

        const userTypeFromSession = sessionStorage.getItem('UserType');

        setUserType(userTypeFromSession);

    }, [sessionStorage.getItem('UserType')]);

    // const [id, setId] = useState("Coach");
    // const [id, setId] = useState("Client");



    return (
        <>
            {
                userType == "Coach" ? <CoachDashboard></CoachDashboard>
                    : <ClientDashboard></ClientDashboard>
            }
        </>
    )
}

export default DashBoard


