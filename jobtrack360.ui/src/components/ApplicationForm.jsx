import React, { useState, useEffect } from 'react';

const ApplicationForm = ({ onSubmit, existingData, onCancel }) => {
  const [formData, setFormData] = useState({
    companyName: '',
    position: '',
    dateApplied: '',
    status: '0', // Default to 'Applied'
    notes: ''
  });

  useEffect(() => {
    if (existingData) {
      const formattedDate = existingData.dateApplied?new Date(existingData.dateApplied).toISOString().split('T')[0]:'';
      setFormData({
        ...existingData,
        dateApplied: formattedDate
      });
    }
  }, [existingData]);

  useEffect(() => {
    if (!existingData) {
      setFormData({ companyName: '', position: '', dateApplied: '', status: '0', notes: '' }); // Reset form
    }
  }, [existingData]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const payload = {
      ...formData,
      status: parseInt(formData.status, 10), 
    }
    onSubmit(payload);
    if(!existingData){
      setFormData({ companyName: '', position: '', dateApplied: '', status: '0', notes: '' }); // Reset form
    }
  };
  

  return (
          <form onSubmit={handleSubmit} className="form">
            <div className="form-row">
              <input name="companyName" placeholder="Enter your company" value={formData.companyName} onChange={handleChange} required disabled={!!existingData}/>
              <input name="position" placeholder="Enter your position" value={formData.position} onChange={handleChange} required disabled={!!existingData}/>
              <input type="date" name="dateApplied" value={formData.dateApplied} onChange={handleChange} required disabled={!!existingData}/>
              <input name="notes" placeholder="Enter your notes" value={formData.notes} onChange={handleChange} />
              <select name="status" placeholder="Select status" value={formData.status} onChange={handleChange}>
                <option value="0">Applied</option>
                <option value="1">Interview</option>
                <option value="2">Offer</option>
                <option value="3">Rejected</option>
              </select>
              <button type="submit">{existingData ? 'Update' : 'Add'}</button>
              {existingData && <button type="button" onClick={onCancel}>Cancel</button>}
            </div>
          </form>

  );
};

export default ApplicationForm;
