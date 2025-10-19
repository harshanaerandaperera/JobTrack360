import axios from 'axios';

// Use empty baseURL since nginx will proxy /api requests to backend
const api = axios.create({
  baseURL: '', // Empty for same-origin requests (nginx proxies /api to backend)
});

export const getApplications = () => api.get('/api/JobApplications');
export const createApplication = (data) => api.post('/api/JobApplications', data);
export const updateApplication = (id, data) => api.put(`/api/JobApplications/${id}`, data);
export const deleteApplication = (id) => api.delete(`/api/JobApplications/${id}`);
export const getApplicationById = (id) => api.get(`/api/JobApplications/${id}`);

export default api;