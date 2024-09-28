namespace WebApp.Areas.Admin.Data
{
    
        public class PhieuMuon_GroupMaDG_DTO
        {
            public DocGia_GroupKey DocGia_GroupKey { get; set; }
            public List<PhieuMuonDTO> DataPhieuMuons { get; set; }
        }

        public class DocGia_GroupKey
        {

            public int MaThe { get; set; }
            public String HoTenDG { get; set; }
            public String SDT { get; set; }
        }

    
}
