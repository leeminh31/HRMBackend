#nullable disable
using Dapper;
//using HRMBackend.Resources;
using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Metadata;

namespace HRMBackend.DataAccess
{
    public abstract class BaseDAO
    {
        #region Properties
        protected NpgsqlConnection Context { get; init; }
        protected NpgsqlTransaction Transaction { get; init; }
        #endregion
    }

    public static class ConfigDapper
    {
        #region Dapper mapping
        /// <summary>
        /// Chức năng: tuỳ biến mapping property name in dapper
        /// </summary>
        public static void Mapping()
        {
            // Set timeout dapper
            SqlMapper.Settings.CommandTimeout = 10000;

            var map1 = new CustomPropertyTypeMap(typeof(Models.CaLamViec), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.CaLamViec), map1);

            var map2 = new CustomPropertyTypeMap(typeof(Models.DangKyCa), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DangKyCa), map2);

            var map3 = new CustomPropertyTypeMap(typeof(Models.DonBu), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DonBu), map3);

            var map4 = new CustomPropertyTypeMap(typeof(Models.DonConNho), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DonConNho), map4);

            var map5 = new CustomPropertyTypeMap(typeof(Models.DonPhep), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DonPhep), map5);

            var map6 = new CustomPropertyTypeMap(typeof(Models.DonTangCa), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DonTangCa), map6);

            var map7 = new CustomPropertyTypeMap(typeof(Models.DuLieuChamCong), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.DuLieuChamCong), map7);

            var map8 = new CustomPropertyTypeMap(typeof(Models.GiaiTrinh), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.GiaiTrinh), map8);

            var map9 = new CustomPropertyTypeMap(typeof(Models.HopDong), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.HopDong), map9);

            var map10 = new CustomPropertyTypeMap(typeof(Models.QuyPhep), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.QuyPhep), map10);

            var map11 = new CustomPropertyTypeMap(typeof(Models.TaiKhoan), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.TaiKhoan), map11);

            var map12 = new CustomPropertyTypeMap(typeof(Models.ChiTietQuyBu), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.ChiTietQuyBu), map12);

            var map13 = new CustomPropertyTypeMap(typeof(Models.ChiTietQuyPhep), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.ChiTietQuyPhep), map13);

            var map14 = new CustomPropertyTypeMap(typeof(Models.NhanVien), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.NhanVien), map14);

            var map15 = new CustomPropertyTypeMap(typeof(Models.PhongBan), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.PhongBan), map15);

            var map16 = new CustomPropertyTypeMap(typeof(Models.QuyBu), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDescriptionFromAttribute(prop) == columnName));
            SqlMapper.SetTypeMap(typeof(Models.QuyBu), map16);
        }

        private static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (ColumnAttribute)Attribute.GetCustomAttribute(member, typeof(ColumnAttribute), false);
            return attrib == null ? null : attrib.Name;
        }
        #endregion
    }
}
