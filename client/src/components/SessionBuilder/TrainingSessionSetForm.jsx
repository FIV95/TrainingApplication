// TrainingSessionSetForm.js
import React, { useState } from 'react';

const TrainingSessionSetForm = ({ onFormSubmit }) => {
    const [reps, setReps] = useState('');
    const [weight, setWeight] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        onFormSubmit({ reps, weight });
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Reps:
                <input type="number" value={reps} onChange={e => setReps(e.target.value)} required />
            </label>
            <label>
                Weight:
                <input type="number" value={weight} onChange={e => setWeight(e.target.value)} required />
            </label>
            <button type="submit">Add Set</button>
        </form>
    );
};

export default TrainingSessionSetForm;
