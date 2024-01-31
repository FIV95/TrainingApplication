import React, { useEffect, useState, createContext, useContext } from 'react';
import TrainingSessionExerciseForm from './TrainingSessionExerciseForm';
import TrainingSessionSetForm from './TrainingSessionSetForm';

const userContextValue = {
    UserId: 1,
    ClientId: 2,
};

const UserContext = createContext(userContextValue);

const TrainingSessionCreate = ({ userId, coachId }) => {
    const [sessions, setSessions] = useState([]);
    const [session, setSession] = useState(null);
    const [sessionExercise, setSessionExercise] = useState(null);
    const [exercises, setExercises] = useState([]);
    const [currentExercise, setCurrentExercise] = useState(null);




    const fetchWeeklyTrainingSessions = async () => {
        const clientId = sessionStorage.getItem('Client-Id');
        const coachId = sessionStorage.getItem('Coach-Id');

        try {
            const response = await fetch(`http://localhost:5252/TrainingSession/WeeklySessions/${clientId}`, {
                method: 'GET',
                headers: {
                    'CoachId': coachId
                }
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const sessions = await response.json();
            setSessions(sessions);
        } catch (error) {
            console.error('Failed to fetch weekly training sessions:', error);
        }
    };

    const handleSessionSubmit = async (formData) => {
        const response = await fetch('http://localhost:5252/TrainingSession', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Session-Id': userContextValue.UserId.toString(),
            },
            body: JSON.stringify({
                DueDate: formData.dueDate,
                ClientId: formData.clientId,
            }),
        });

        const newSession = await response.json();
        setSession(newSession);
    };

    const handleExerciseSubmit = async (formData) => {
        const response = await fetch(`TrainingSession/${session.TrainingSessionId}/TrainingSessionExercise`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ExerciseId: formData.exerciseId,
            }),
        });

        const newSessionExercise = await response.json();
        setSessionExercise(prevExercises => [...prevExercises, newSessionExercise]);
    };

    const handleSetSubmit = async (formData) => {
        const response = await fetch(`/api/TrainingSessionExercise/${currentExercise.TrainingSessionExerciseId}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Reps: formData.reps,
                Weight: formData.weight,
            }),
        });

        const newSet = await response.json();
    };

    const fetchAllExercises = async () => {
        const response = await fetch('/api/Exercise');
        const exercises = await response.json();
        setExercises(exercises);
    };

    useEffect(() => {
        fetchWeeklyTrainingSessions();
        fetchAllExercises();
    }, []);

    return (

    );
            }
export default TrainingSessionCreate;
