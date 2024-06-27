//using InfoDev.IOffice.ICommon.Enums;
//using InfoDev.IOffice.ICommon.Results;
//using InfoDev.IOffice.ViewModels.HumanResources;
//using InfoDev.IOffice.ViewModels.Others;
//using InfoDev.IOffice.ViewModels.Users;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace EmailSender
//{
//    public interface IMailSettingsBL
//    {
//        Task<DataResult> Create(MailSettingsViewModel model, string controllerName);
//        Task<DataResult> Update(MailSettingsViewModel data, ActionPermissionViewModel permission, string controllerName);
//        Task<DataResult> Delete(int Id, int userId, ActionPermissionViewModel permission, string controllerName);
//        Task<MailSettingsViewModel> GetData(int Id);
//        Task<DataResult> Approve(int Id, int userId, string controllerName);
//        Task<GridIndexData> GetSearchData(string pq_filter, int pq_curPage, int pq_rPP);
//        Task<GridIndexData> GetIndexData(DisplayStatus disp, string pq_filter, int pq_curPage, int pq_rPP);
//        Task<List<MailSettingsViewModel>> GetData(DisplayStatus displayStatus);
//        Task<MailSettingsViewModel> GetDefaultMailSettings();

//    }
//}
