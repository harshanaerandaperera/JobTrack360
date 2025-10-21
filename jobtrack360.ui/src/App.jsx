import React, { useEffect, useState } from 'react';
import { getApplications, createApplication, updateApplication, deleteApplication } from './services/api';
import ApplicationForm from './components/ApplicationForm';
import ApplicationsTable from './components/ApplicationsTable';

const App = () => {
  const [applications, setApplications] = useState([]);
  const [editingApp, setEditingApp] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 5;

  const fetchData = async () => {
    try {
      const response = await getApplications();
      setApplications(response.data);
    } catch (err) {
      console.error('Error fetching applications', err);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async (formData) => {
    try {
      if (editingApp) {
        await updateApplication(editingApp.id, formData);
      } else {
        await createApplication(formData);
      }
      setEditingApp(null);
      fetchData();
    } catch (err) {
      console.error('Error submitting form', err);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this application?')) {
      try {
        await deleteApplication(id);
        fetchData();
      } catch (err) {
        console.error('Error deleting application:', err.message);
      }
    }
  };

  // Pagination
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentApplications = applications.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(applications.length / itemsPerPage);
  const pageNumbers = Array.from({ length: totalPages }, (_, i) => i + 1);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="app-container">
      <h1>JobTrack360 | Job Application Tracker : V 1.0</h1>
      <ApplicationForm
        onSubmit={handleSubmit}
        existingData={editingApp}
        onCancel={() => setEditingApp(null)}
      />
      <ApplicationsTable
        applications={currentApplications}
        onEdit={setEditingApp}
        onDelete={handleDelete}
      />
      
      {/* Pagination Controls */}
      <div className="pagination">
        {pageNumbers.map((number) => (
          <button
            key={number}
            onClick={() => handlePageChange(number)}
            className={currentPage === number ? 'active' : ''}
          >
            {number}
          </button>
        ))}
      </div>
    </div>
  );
};

export default App;
