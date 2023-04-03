using Microsoft.EntityFrameworkCore;
using TMS.BLL.Interfaces;
using TMS.DAL.Entities;
using TMS.DAL.Interfaces;

namespace TMS.BLL.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            var enrollmentRepository = _unitOfWork.GetRepository<Enrollment>();
            return await enrollmentRepository.GetAllAsync(include: e => e
                .Include(e => e.Course)
                .Include(e => e.Trainee));
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            var enrollmentRepository = _unitOfWork.GetRepository<Enrollment>();
            return await enrollmentRepository.GetSingleByAsync(e => e.Id == id, include: e => e
                .Include(e => e.Course)
                .Include(e => e.Trainee));
        }


        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            var enrollmentRepository = _unitOfWork.GetRepository<Enrollment>();
            await enrollmentRepository.AddAsync(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            var enrollmentRepository = _unitOfWork.GetRepository<Enrollment>();
            enrollmentRepository.Update(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEnrollmentAsync(Enrollment enrollment)
        {
            var enrollmentRepository = _unitOfWork.GetRepository<Enrollment>();
            enrollmentRepository.Delete(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
