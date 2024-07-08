using DeerCoffeeShop.Domain.Common.Interfaces;
using DeerCoffeeShop.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace DeerCoffeeShop.Infrastructure.Persistence.Configurations
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork
    {

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RestaurantChain> Restaurants { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public virtual DbSet<Attendence> Attendences { get; set; }
        public virtual DbSet<LateRecord> LateRecords { get; set; }
        public virtual DbSet<RestaurantChain> RestaurantChains { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            _ = modelBuilder.ApplyConfiguration(new EmployeeShiftConfiguration());
            _ = modelBuilder.ApplyConfiguration(new AttendenceConfiguration());
            ConfigureModel(modelBuilder);

        }
        private static void ConfigureModel(ModelBuilder modelBuilder)
        {
            #region Employee
            _ = modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    ID = 1,
                    Name = "Admin",
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Role
                {
                    ID = 2,
                    Name = "Manger",
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Role
                {
                    ID = 3,
                    Name = "Employee",
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                });
            _ = modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    ID = "1",

                    FullName = "Admin",
                    Email = "Admin@gmail.com",
                    Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                    PhoneNumber = "0123456789",
                    Address = "HCM",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now,
                    RoleID = 1,
                    IsActive = true,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Employee
                {
                    ID = "2",

                    FullName = "Manger",
                    Email = "Manger@gmail.com",
                    Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                    PhoneNumber = "0123456789",
                    Address = "HCM",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now,
                    RoleID = 2,
                    IsActive = true,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Employee
                {
                    ID = "3",

                    FullName = "Employee3",
                    Email = "Employee3@gmail.com",
                    Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                    PhoneNumber = "0123456789",
                    Address = "HCM",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now,
                    RoleID = 3,
                    IsActive = true,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Employee
                {
                    ID = "4",

                    FullName = "Employee4",
                    Email = "Employee4@gmail.com",
                    Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                    PhoneNumber = "0123456789",
                    Address = "HCM",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now,
                    RoleID = 3,
                    IsActive = true,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Employee
                {
                    ID = "5",
                    FullName = "Employee5",
                    Email = "Employee5@gmail.com",
                    Password = "$2a$11$dRZA37NpS.thXR9anJXBZehaTb7ezji2i2E5WbHGA2cwMeW4wEXAy",
                    PhoneNumber = "0123456789",
                    Address = "HCM",
                    DateOfBirth = DateTime.Now,
                    DateJoined = DateTime.Now,
                    RoleID = 3,
                    IsActive = true,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                });
            #endregion
            #region Form    
            _ = modelBuilder.Entity<Form>().HasData(
            new Form
            {
                ID = Guid.NewGuid().ToString(),
                EmployeeID = null,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Content = "Feedback",
                Date = DateTime.Now,
                IsApproved = false,
                NguoiTaoID = "1",
                NgayTao = DateTime.Now,
            },
            new Form
            {
                ID = Guid.NewGuid().ToString(),
                EmployeeID = null,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Content = "Feedback",
                Date = DateTime.Now,
                IsApproved = true,
                NguoiTaoID = "1",
                NgayTao = DateTime.Now,
            },
            new Form
            {
                ID = Guid.NewGuid().ToString(),
                EmployeeID = null,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Content = "Feedback",
                Date = DateTime.Now,
                IsApproved = true,
                NguoiTaoID = "1",
                NgayTao = DateTime.Now,
            },
            new Form
            {
                ID = Guid.NewGuid().ToString(),
                EmployeeID = null,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Content = "Feedback",
                Date = DateTime.Now,
                IsApproved = true,
                NguoiTaoID = "1",
                NgayTao = DateTime.Now,
            },
            new Form
            {
                ID = Guid.NewGuid().ToString(),
                EmployeeID = null,
                FormType = Domain.Enums.FormTypeEnum.JOB_APPLICATION,
                Content = "Feedback",
                Date = DateTime.Now,
                IsApproved = true,
                NguoiTaoID = "1",
                NgayTao = DateTime.Now,
            });
            #endregion
            #region RestaurantChain
            _ = modelBuilder.Entity<RestaurantChain>().HasData(
                new RestaurantChain
                {
                    ID = "ChuoiNhaHang1",
                    RestaurantChainHQAddress = "HCM",
                    RestaurantChainName = "DeerCoffee",
                    RestaurantChain_AdminID = "1",
                    RestaurantChainTotalBranches = 1,
                    RestaurantChainTotalEmployees = 1,
                    RestaurantChainType = "Coffee",
                    IsDeleted = false,
                    NgayCapNhatCuoi = DateTime.Now,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new RestaurantChain
                {
                    ID = "2",
                    RestaurantChainHQAddress = "HCM",
                    RestaurantChainName = "DeerFastfood",
                    RestaurantChain_AdminID = "2",
                    RestaurantChainTotalBranches = 1,
                    RestaurantChainTotalEmployees = 1,
                    RestaurantChainType = "Food",
                    IsDeleted = false,
                    NgayCapNhatCuoi = DateTime.Now,
                    NguoiTaoID = "2",
                    NgayTao = DateTime.Now,
                });
            #endregion
            #region Restaurant
            _ = modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    ID = "NhaHang1",
                    RestaurantChainID = "ChuoiNhaHang1",
                    RestaurantName = "DeerCoffee",
                    RestaurantAddress = "HCM",
                    ManagerID = "1",
                    TotalEmployees = 1,
                    IsDeleted = false,
                    NgayCapNhatCuoi = DateTime.Now,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Restaurant
                {
                    ID = "NhaHang2",
                    RestaurantChainID = "ChuoiNhaHang1",
                    RestaurantName = "DeerCoffee",
                    RestaurantAddress = "HCM",
                    ManagerID = "1",
                    TotalEmployees = 1,
                    IsDeleted = false,
                    NgayCapNhatCuoi = DateTime.Now,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                },
                new Restaurant
                {
                    ID = "NhaHang3",
                    RestaurantChainID = "ChuoiNhaHang1",
                    RestaurantName = "DeerCoffee",
                    RestaurantAddress = "HCM",
                    ManagerID = "1",
                    TotalEmployees = 1,
                    IsDeleted = false,
                    NgayCapNhatCuoi = DateTime.Now,
                    NguoiTaoID = "1",
                    NgayTao = DateTime.Now,
                });
            #endregion
            #region shift
            _ = modelBuilder.Entity<Shift>().HasData(
                               new Shift
                               {
                                   ID = 1,
                                   Name = "Sang 07-15",
                                   ShiftStart = 7,
                                   ShiftEnd = 15,
                                   ShiftDescription = "Ca sang 7h-15h",
                                   IsActive = true,
                                   NguoiTaoID = "1",
                                   NgayTao = DateTime.Now,
                               },
                               new Shift
                               {
                                   ID = 2,
                                   Name = "Chieu 15-22",
                                   ShiftStart = 15,
                                   ShiftEnd = 22,
                                   ShiftDescription = "Ca chieu 15h-22h",
                                   IsActive = true,
                                   NguoiTaoID = "1",
                                   NgayTao = DateTime.Now,
                               }, new Shift
                               {
                                   ID = 3,
                                   Name = "Trua 10-16",
                                   ShiftStart = 15,
                                   ShiftEnd = 22,
                                   ShiftDescription = "Ca trua 10h-16h",
                                   IsActive = true,
                                   NguoiTaoID = "1",
                                   NgayTao = DateTime.Now,
                               });

            #endregion
            #region EmployeeShift
            _ = modelBuilder.Entity<EmployeeShift>().HasData(
                               new EmployeeShift
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   EmployeeID = "1",
                                   RestaurantID = "NhaHang1",

                                   DateOfWork = DateOnly.FromDateTime(DateTime.Now),
                                   Month = DateTime.Now.Month,
                                   Year = DateTime.Now.Year,
                                   IsOnTime = false,
                                   NguoiTaoID = "1",
                                   Status = Domain.Enums.EmployeeShiftStatus.Absent,
                                   NgayTao = DateTime.Now,
                               },
                               new EmployeeShift
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   EmployeeID = "1",
                                   RestaurantID = "NhaHang2",

                                   DateOfWork = DateOnly.FromDateTime(DateTime.Now),
                                   Month = DateTime.Now.Month,
                                   Year = DateTime.Now.Year,
                                   IsOnTime = false,
                                   NguoiTaoID = "1",
                                   Status = Domain.Enums.EmployeeShiftStatus.Absent,
                                   NgayTao = DateTime.Now,
                               },
                               new EmployeeShift
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   EmployeeID = "3",
                                   RestaurantID = "NhaHang3",

                                   DateOfWork = DateOnly.FromDateTime(DateTime.Now),
                                   Month = DateTime.Now.Month,
                                   Year = DateTime.Now.Year,
                                   IsOnTime = false,
                                   NguoiTaoID = "1",
                                   Status = Domain.Enums.EmployeeShiftStatus.Absent,
                                   NgayTao = DateTime.Now,
                               },
                               new EmployeeShift
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   EmployeeID = "1",
                                   RestaurantID = "NhaHang1",

                                   DateOfWork = DateOnly.FromDateTime(DateTime.Now),
                                   Month = DateTime.Now.Month,
                                   Year = DateTime.Now.Year,
                                   IsOnTime = false,
                                   NguoiTaoID = "1",
                                   Status = Domain.Enums.EmployeeShiftStatus.Absent,
                                   NgayTao = DateTime.Now,
                               });
            #endregion

        }
    }
}


