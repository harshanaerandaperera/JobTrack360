import React from 'react';

const ApplicationsTable = ({ applications, onEdit, onDelete }) => {
  const JobStatusLabels = {
  0: 'Applied',
  1: 'Interview',
  2: 'Offer',
  3: 'Rejected'
};
  return (
    <table>
      <thead>
        <tr>
          <th>Company</th>
          <th>Position</th>
          <th>Note</th>
          <th>Status</th>
          <th>Date Applied</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {applications.map((app) => (
          <tr key={app.id}>
            <td>{app.companyName}</td>
            <td>{app.position}</td>
            <td>{app.notes}</td>
            <td>{JobStatusLabels[app.status]}</td>
            <td>{app.dateApplied?.split('T')[0]}</td>
            <td>
              <button onClick={() => onEdit(app)}>Edit</button>
              <button onClick={() => onDelete(app.id)} style={{ marginLeft: '10px' }}>Delete</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default ApplicationsTable;
