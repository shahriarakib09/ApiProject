using ApiProject.Views.StudentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using ApiProject.Features.Helper;

namespace ApiProject.Features.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionstring;
        public StudentRepository(IConfiguration config)
        {
            _connectionstring = config.GetSection("Api")["ConnectionString"];
        }
        public async Task<IReadOnlyList<StudentView>> GetAllStudentListAsync()
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
                {
                    var results = await _dbConnection.QueryAsync<StudentView>("[dbo].[GetAllStudents]", commandType: CommandType.StoredProcedure);
                    return results.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> CreateStudents(StudentView _studentView)
        {
            string ret = string.Empty;

            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
                {
                    _dbConnection.Open();
                    using (var transaction = _dbConnection.BeginTransaction())
                    {
                        try
                        {
                            DynamicParameters parameters = new();
                            parameters.Add("@Id", 0 , DbType.Int32);
                            parameters.Add("@StudentName", _studentView.StudentName, DbType.String);
                            parameters.Add("@Email", _studentView.Email, DbType.String);
                            parameters.Add("@StudentRoll", _studentView.StudentRoll, DbType.String);
                            parameters.Add("@StudentAddress", _studentView.StudentAddress, DbType.String);
                            parameters.Add("@TransactionType", "INSERT", DbType.String);

                            var results = await _dbConnection.QueryAsync<string>("[dbo].[Students_Post]", parameters, transaction, commandType: CommandType.StoredProcedure);
                            string number = results.FirstOrDefault().ToString();

                            transaction.Commit();
                            return number;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }

                    }


                }
            }
            catch(Exception ex)
            {
                throw;
            }
            




        }
    } 
}