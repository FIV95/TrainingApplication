// TrainingSessionExerciseForm.js
import React, { useState } from 'react';

const TrainingSessionExerciseForm = ({ onFormSubmit, onExerciseSelect, exercises }) => {
    const [exerciseId, setExerciseId] = useState('');

    const handleExerciseChange = (e) => {
        setExerciseId(e.target.value);
        const selectedExercise = exercises.find(exercise => exercise.ExerciseId === e.target.value);
        onExerciseSelect(selectedExercise);
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        onFormSubmit({ exerciseId });
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Exercise:
                <select value={exerciseId} onChange={handleExerciseChange} required>
                    <option value="">Select an exercise</option>
                    {exercises.map(exercise => (
                        <option key={exercise.ExerciseId} value={exercise.ExerciseId}>
                            {exercise.Name}
                        </option>
                    ))}
                </select>
            </label>
            <button type="submit">Add Exercise</button>
        </form>
    );
};

export default TrainingSessionExerciseForm;
