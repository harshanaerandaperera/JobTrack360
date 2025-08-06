import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7296', // Backend server 
});

export const getApplications = () => api.get('/api/JobApplications');
export const createApplication = (data) => api.post('/api/JobApplications', data);
export const updateApplication = (id, data) => api.put(`/api/JobApplications/${id}`, data);
export const deleteApplication = (id) => api.delete(`/api/JobApplications/${id}`);
export const getApplicationById = (id) => api.get(`/api/JobApplications/${id}`);

export default api;