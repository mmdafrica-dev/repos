using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MMD_Website_AfricaC.Models
{
    public class MtmsContext : DbContext
    {
        public DbSet<Part> Parts { get; }
        public DbSet<Customer> Customers { get; }
        public DbSet<Supplier> Suppliers { get; }

    }

    public class Part
    {
        [Key, Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public int StudentID { get; }

        [Required, StringLength(40), Display(Name = "Last Name")]
        public string LastName { get; }

        [Required, StringLength(20), Display(Name = "First Name")]
        public string FirstName { get; }

        [EnumDataType(typeof(AcademicYear)), Display(Name = "Academic Year")]
        public AcademicYear Year { get; }

        public virtual ICollection<Enrollment> Enrollments { get; }
    }

    public class Customer
    {

    }

    public class Supplier
    {

    }

    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal? Grade { get; set; }
    }


    public enum AcademicYear
    {
        Freshman,
        Sophomore,
        Junior,
        Senior
    }

    public enum CLED_DATA
    {

    }
    public enum FURD_DATA { }
    public enum COMP_DATA { }
    public enum MISC_DATA { }
    public enum EJRN_DATA { }
    public enum EJRN_DATA_COPY { }
    public enum PART_DATA { }
    public enum ORDS_DATA { }
    public enum CJRN_DATA { }
    public enum OACT_DATA { }
    public enum SMOV_DATA { }
    public enum INVS_DATA { }
    public enum SCHD_DATA { }
    public enum ELED_DATA { }
    public enum GOOD_DATA { }
    public enum INVL_DATA { }
    public enum MOPR_DATA { }
    public enum DOCS_DATA { }
    public enum PATH_DATA { }
    public enum BTCH_DATA { }
    public enum HIST_DATA { }
    public enum MMD_ME_BINS { }
    public enum MMD_JOB_EJRN_DATA { }
    public enum MMD_ME_BINS_OLD { }
    public enum MMD_ME_BINS_TEST { }
    public enum BOOK_DATA { }
    public enum CSPT_DATA { }
    public enum WOPR_DATA { }
    public enum CSUM_DATA { }
    public enum OVFL_DATA { }
    public enum PURH_DATA { }
    public enum STAG_DATA { }
    public enum ARRY_DATA { }
    public enum BKTX_DATA { }
    public enum PPMH_DATA { }
    public enum DESP_DATA { }
    public enum ALOC_DATA { }
    public enum MMD_STOCK_PROV { }
    public enum TAGS_DATA { }
    public enum FURE_DATA { }
    public enum LDOC_DATA { }
    public enum NARR_DATA { }
    public enum PRAM_DATA { }
    public enum INVV_DATA { }
    public enum ME_DOCS { }
    public enum CREC_DATA { }
    public enum ADDR_DATA { }
    public enum MMD_AT_USERTEMPSCH { }
    public enum MMD_PRAM_TMP { }
    public enum SYS_EXPORT_SCHEMA_01 { }
    public enum ADIS_DATA { }
    public enum MMD_ME_STK_TO_INV_PRICE { }
    public enum ACCA_DATA { }
    public enum CTOT_DATA { }
    public enum MMD_AT_CHECKEXACT { }
    public enum MMD_AT_CHECKINOUT { }
    public enum CREF_DATA { }
    public enum EROR_DATA { }
    public enum SCAL_DATA { }
    public enum SYS2_DATA { }
    public enum USER_DATA { }
    public enum WORD_DATA { }
    public enum DDIC_DATA { }
    public enum ISSU_DATA { }
    public enum BINS_DATA { }
    public enum DNAR_DATA { }
    public enum MMD_GTT_BOM { }
    public enum MMD_NOTICES { }
    public enum CHGS_DATA { }
    public enum ACCM_DATA { }
    public enum EXCP_DATA { }
    public enum FINANCE_SCAL { }
    public enum INAP_DATA { }
    public enum MMD_AT_USERSPEDAY { }
    public enum MMD_TABLE_DATA { }
    public enum PAST_DATA { }
    public enum PLIN_DATA { }
    public enum SUGG_DATA { }
    public enum LINK_DATA { }
    public enum PROG_DATA { }
    public enum MMD_INT_ACCESS { }
    public enum PLIN_COPY { }
    public enum MMD_STOCK_CONTROL { }
    public enum CURR_DATA { }
    public enum EGLM_DATA { }
    public enum SRTN_DATA { }
    public enum ACCR_DATA { }
    public enum BROWSE_DATA { }
    public enum CONN_DATA { }
    public enum PCAU_DATA { }
    public enum MMD_ICB20 { }
    public enum MMD_ICB20_B { }
    public enum MMD_ICB20_BCOPY { }
    public enum MMD_ICB20_COPY { }
    public enum MMD_ICB20_OLD { }
    public enum MMD_ICB20_WIP { }
    public enum MMD_ME_STK_TO_INV_PRICE_TEMP { }
    public enum SYS1_DATA { }
    public enum TAEM_DATA { }
    public enum MMD_AT_USERINFO { }
    public enum MMD_ICB20_A { }
    public enum PCHG_DATA { }
    public enum SYNC_DATA { }
    public enum SYS6_DATA { }
    public enum FINC_DATA { }
    public enum KEYS_DATA { }
    public enum MMD_INT_MENU { }
    public enum MMD_PART_SOURCE_CHANGED { }
    public enum MMD_SP_MACHINELIST { }
    public enum RATE_DATA { }
    public enum WORK_DATA { }
    public enum ABPX_DATA { }
    public enum AUDR_DATA { }
    public enum BATP_DATA { }
    public enum BKAC_DATA { }
    public enum CCUR_DATA { }
    public enum CHCD_DATA { }
    public enum ECHG_DATA { }
    public enum EDCB_DATA { }
    public enum EMPL_DATA { }
    public enum EREP_DATA { }
    public enum ISSS_DATA { }
    public enum MBNK_DATA { }
    public enum MEMO_DATA { }
    public enum MMDAFRICA { }
    public enum PDEF_DATA { }
    public enum PPMB_DATA { }
    public enum PROJ_DATA { }
    public enum RESR_DATA { }
    public enum REST_DATA { }
    public enum RTLN_DATA { }
    public enum TABO_DATA { }
    public enum TASH_DATA { }
    public enum TBCL_DATA { }
    public enum WIPH_DATA { }
    public enum DEFAULTHEADER { }
    public enum DEFAULTVALUES { }
    public enum MMD_AT_DEPARTMENTS { }
    public enum MMD_AT_HOLIDAYS { }
    public enum MMD_AT_INFO { }
    public enum MMD_AT_LEAVE { }
    public enum MMD_AT_LEAVECLASS { }
    public enum MMD_AT_OVERTIME { }
    public enum MMD_AT_SCHCLASS { }
    public enum MMD_AT_UPDATER { }
    public enum MMD_BOM_PSP { }
    public enum MMD_ICB20_SERVERS { }
    public enum MMD_ICB20_USERS { }
    public enum MMD_INT_EXCH { }
    public enum MMD_INT_STAFF { }
    public enum MMD_INT_USERS { }
    public enum MMD_PORELEASE { }
    public enum MMD_PSP_PRICINGSYSTEM { }

    //empty Tables
    public enum ACCB_DATA
    {
        //Empty 
    }
    public enum ACRF_DATA
    {
        //Empty 
    }
    public enum ACRU_DATA
    {
        //Empty 
    }
    public enum ACSA_DATA
    {
        //Empty 
    }
    public enum ACSB_DATA
    {
        //Empty 
    }
    public enum ACSC_DATA
    {
        //Empty 
    }
    public enum ACSD_DATA
    {
        //Empty 
    }
    public enum ACSE_DATA
    {
        //Empty 
    }
    public enum ACSF_DATA
    {
        //Empty 
    }
    public enum ACSG_DATA
    {
        //Empty 
    }
    public enum ACSH_DATA
    {
        //Empty 
    }
    public enum ACSK_DATA
    {
        //Empty 
    }
    public enum ACSL_DATA
    {
        //Empty 
    }
    public enum ACSM_DATA
    {
        //Empty 
    }
    public enum ACSO_DATA
    {
        //Empty 
    }
    public enum ACSP_DATA
    {
        //Empty 
    }
    public enum ACSQ_DATA
    {
        //Empty 
    }
    public enum ACSR_DATA
    {
        //Empty 
    }
    public enum ACSS_DATA
    {
        //Empty 
    }
    public enum ACST_DATA
    {
        //Empty 
    }
    public enum ACSU_DATA
    {
        //Empty 
    }
    public enum ACSV_DATA
    {
        //Empty 
    }
    public enum ACSW_DATA
    {
        //Empty 
    }
    public enum ACSX_DATA
    {
        //Empty 
    }
    public enum ACSY_DATA
    {
        //Empty 
    }
    public enum ACSZ_DATA
    {
        //Empty 
    }
    public enum AFED_DATA
    {
        //Empty 
    }
    public enum AFRT_DATA
    {
        //Empty 
    }
    public enum AGRC_DATA
    {
        //Empty 
    }
    public enum ALCM_DATA
    {
        //Empty 
    }
    public enum ALOT_DATA
    {
        //Empty 
    }
    public enum ALVR_DATA
    {
        //Empty 
    }
    public enum APSP_DATA
    {
        //Empty 
    }
    public enum APST_DATA
    {
        //Empty 
    }
    public enum ARFA_DATA
    {
        //Empty 
    }
    public enum ARFB_DATA
    {
        //Empty 
    }
    public enum ARFC_DATA
    {
        //Empty 
    }
    public enum ARFD_DATA
    {
        //Empty 
    }
    public enum ARPD_DATA
    {
        //Empty 
    }
    public enum ARPE_DATA
    {
        //Empty 
    }
    public enum ASKL_DATA
    {
        //Empty 
    }
    public enum ATPA_DATA
    {
        //Empty 
    }
    public enum ATPD_DATA
    {
        //Empty 
    }
    public enum ATPE_DATA
    {
        //Empty 
    }
    public enum ATPF_DATA
    {
        //Empty 
    }
    public enum ATPR_DATA
    {
        //Empty 
    }
    public enum ATPS_DATA
    {
        //Empty 
    }
    public enum ATTH_DATA
    {
        //Empty 
    }
    public enum ATTR_DATA
    {
        //Empty 
    }
    public enum ATTX_DATA
    {
        //Empty 
    }
    public enum AUTH_DATA
    {
        //Empty 
    }
    public enum AVAL_DATA
    {
        //Empty 
    }
    public enum BANK_DATA
    {
        //Empty 
    }
    public enum BLOT_DATA
    {
        //Empty 
    }
    public enum BOMC_DATA
    {
        //Empty 
    }
    public enum BOML_DATA
    {
        //Empty 
    }
    public enum BOOP_DATA
    {
        //Empty 
    }
    public enum BPRO_DATA
    {
        //Empty 
    }
    public enum BPUT_DATA
    {
        //Empty 
    }
    public enum BROW_DATA
    {
        //Empty 
    }
    public enum BUDG_DATA
    {
        //Empty 
    }
    public enum BUSC_DATA
    {
        //Empty 
    }
    public enum BUSP_DATA
    {
        //Empty 
    }
    public enum BUYE_DATA
    {
        //Empty 
    }
    public enum BUYO_DATA
    {
        //Empty 
    }
    public enum CACT_DATA
    {
        //Empty 
    }
    public enum CALT_DATA
    {
        //Empty 
    }
    public enum CAPS_DATA
    {
        //Empty 
    }
    public enum CASH_DATA
    {
        //Empty 
    }
    public enum CATS_DATA
    {
        //Empty 
    }
    public enum CBID_DATA
    {
        //Empty 
    }
    public enum CBTB_DATA
    {
        //Empty 
    }
    public enum CCTR_DATA
    {
        //Empty 
    }
    public enum CDEF_DATA
    {
        //Empty 
    }
    public enum CDSD_DATA
    {
        //Empty 
    }
    public enum CDSG_DATA
    {
        //Empty 
    }
    public enum CESI_DATA
    {
        //Empty 
    }
    public enum CFAC_DATA
    {
        //Empty 
    }
    public enum CFAD_DATA
    {
        //Empty 
    }
    public enum CFAN_DATA
    {
        //Empty 
    }
    public enum CFAX_DATA
    {
        //Empty 
    }
    public enum CHIS_DATA
    {
        //Empty 
    }
    public enum CIRC_DATA
    {
        //Empty 
    }
    public enum CMAT_DATA
    {
        //Empty 
    }
    public enum CMCD_DATA
    {
        //Empty 
    }
    public enum CMCS_DATA
    {
        //Empty 
    }
    public enum CMOA_DATA
    {
        //Empty 
    }
    public enum CMOH_DATA
    {
        //Empty 
    }
    public enum CMOK_DATA
    {
        //Empty 
    }
    public enum CMOL_DATA
    {
        //Empty 
    }
    public enum CMOR_DATA
    {
        //Empty 
    }
    public enum CNFG_DATA
    {
        //Empty 
    }
    public enum CNFM_DATA
    {
        //Empty 
    }
    public enum CNH1_DATA
    {
        //Empty 
    }
    public enum CNH2_DATA
    {
        //Empty 
    }
    public enum CNH3_DATA
    {
        //Empty 
    }
    public enum CNNO_DATA
    {
        //Empty 
    }
    public enum CONB_DATA
    {
        //Empty 
    }
    public enum CONF_DATA
    {
        //Empty 
    }
    public enum CONG_DATA
    {
        //Empty 
    }
    public enum CONH_DATA
    {
        //Empty 
    }
    public enum CONX_DATA
    {
        //Empty 
    }
    public enum COPY_DATA
    {
        //Empty 
    }
    public enum CORD_DATA
    {
        //Empty 
    }
    public enum COVR_DATA
    {
        //Empty 
    }
    public enum CPDD_DATA
    {
        //Empty 
    }
    public enum CPMX_DATA
    {
        //Empty 
    }
    public enum CPOL_DATA
    {
        //Empty 
    }
    public enum CUCA_DATA
    {
        //Empty 
    }
    public enum CUPF_DATA
    {
        //Empty 
    }
    public enum CUSE_DATA
    {
        //Empty 
    }
    public enum CVAR_DATA
    {
        //Empty 
    }
    public enum CXDH_DATA
    {
        //Empty 
    }
    public enum DACT_DATA
    {
        //Empty 
    }
    public enum DCHG_DATA
    {
        //Empty 
    }
    public enum DCNR_DATA
    {
        //Empty 
    }
    public enum DCON_DATA
    {
        //Empty 
    }
    public enum DCUM_DATA
    {
        //Empty 
    }
    public enum DDES_DATA
    {
        //Empty 
    }
    public enum DEMH_DATA
    {
        //Empty 
    }
    public enum DEMS_DATA
    {
        //Empty 
    }
    public enum DEXP_DATA
    {
        //Empty 
    }
    public enum DFCC_DATA
    {
        //Empty 
    }
    public enum DISC_DATA
    {
        //Empty 
    }
    public enum DIVI_DATA
    {
        //Empty 
    }
    public enum DOPS_DATA
    {
        //Empty 
    }
    public enum DOSS_DATA
    {
        //Empty 
    }
    public enum DPAK_DATA
    {
        //Empty 
    }
    public enum DPAV_DATA
    {
        //Empty 
    }
    public enum DPDB_DATA
    {
        //Empty 
    }
    public enum DPRO_DATA
    {
        //Empty 
    }
    public enum DRAW_DATA
    {
        //Empty 
    }
    public enum DRPD_DATA
    {
        //Empty 
    }
    public enum DRPH_DATA
    {
        //Empty 
    }
    public enum DRPM_DATA
    {
        //Empty 
    }
    public enum DRPS_DATA
    {
        //Empty 
    }
    public enum DRPX_DATA
    {
        //Empty 
    }
    public enum DRWG_DATA
    {
        //Empty 
    }
    public enum DSET_DATA
    {
        //Empty 
    }
    public enum DSGN_DATA
    {
        //Empty 
    }
    public enum DSTA_DATA
    {
        //Empty 
    }
    public enum EBAA_DATA
    {
        //Empty 
    }
    public enum EBS1_DATA
    {
        //Empty 
    }
    public enum EBS2_DATA
    {
        //Empty 
    }
    public enum EBSH_DATA
    {
        //Empty 
    }
    public enum EBSQ_DATA
    {
        //Empty 
    }
    public enum EBSV_DATA
    {
        //Empty 
    }
    public enum EBUD_DATA
    {
        //Empty 
    }
    public enum ECAH_DATA
    {
        //Empty 
    }
    public enum ECAL_DATA
    {
        //Empty 
    }
    public enum ECAM_DATA
    {
        //Empty 
    }
    public enum ECAT_DATA
    {
        //Empty 
    }
    public enum ECAV_DATA
    {
        //Empty 
    }
    public enum ECBD_DATA
    {
        //Empty 
    }
    public enum ECBL_DATA
    {
        //Empty 
    }
    public enum ECBY_DATA
    {
        //Empty 
    }
    public enum ECCC_DATA
    {
        //Empty 
    }
    public enum ECCD_DATA
    {
        //Empty 
    }
    public enum ECCH_DATA
    {
        //Empty 
    }
    public enum ECCO_DATA
    {
        //Empty 
    }
    public enum ECCU_DATA
    {
        //Empty 
    }
    public enum ECDQ_DATA
    {
        //Empty 
    }
    public enum ECEF_DATA
    {
        //Empty 
    }
    public enum ECHD_DATA
    {
        //Empty 
    }
    public enum ECLH_DATA
    {
        //Empty 
    }
    public enum ECLN_DATA
    {
        //Empty 
    }
    public enum ECMC_DATA
    {
        //Empty 
    }
    public enum ECMD_DATA
    {
        //Empty 
    }
    public enum ECMH_DATA
    {
        //Empty 
    }
    public enum ECML_DATA
    {
        //Empty 
    }
    public enum ECMM_DATA
    {
        //Empty 
    }
    public enum ECMT_DATA
    {
        //Empty 
    }
    public enum ECMU_DATA
    {
        //Empty 
    }
    public enum ECOA_DATA
    {
        //Empty 
    }
    public enum ECOD_DATA
    {
        //Empty 
    }
    public enum ECON_DATA
    {
        //Empty 
    }
    public enum ECOP_DATA
    {
        //Empty 
    }
    public enum ECOV_DATA
    {
        //Empty 
    }
    public enum ECPA_DATA
    {
        //Empty 
    }
    public enum ECPH_DATA
    {
        //Empty 
    }
    public enum ECPI_DATA
    {
        //Empty 
    }
    public enum ECPJ_DATA
    {
        //Empty 
    }
    public enum ECPN_DATA
    {
        //Empty 
    }
    public enum ECPS_DATA
    {
        //Empty 
    }
    public enum ECPT_DATA
    {
        //Empty 
    }
    public enum ECRL_DATA
    {
        //Empty 
    }
    public enum ECRP_DATA
    {
        //Empty 
    }
    public enum ECRR_DATA
    {
        //Empty 
    }
    public enum ECSC_DATA
    {
        //Empty 
    }
    public enum ECSH_DATA
    {
        //Empty 
    }
    public enum ECST_DATA
    {
        //Empty 
    }
    public enum ECUC_DATA
    {
        //Empty 
    }
    public enum ECUO_DATA
    {
        //Empty 
    }
    public enum ECUP_DATA
    {
        //Empty 
    }
    public enum ECUT_DATA
    {
        //Empty 
    }
    public enum ECVA_DATA
    {
        //Empty 
    }
    public enum ECVD_DATA
    {
        //Empty 
    }
    public enum ECVH_DATA
    {
        //Empty 
    }
    public enum ECVL_DATA
    {
        //Empty 
    }
    public enum ECVV_DATA
    {
        //Empty 
    }
    public enum EDCA_DATA
    {
        //Empty 
    }
    public enum EDCC_DATA
    {
        //Empty 
    }
    public enum EDCD_DATA
    {
        //Empty 
    }
    public enum EDCE_DATA
    {
        //Empty 
    }
    public enum EDCF_DATA
    {
        //Empty 
    }
    public enum EGAL_DATA
    {
        //Empty 
    }
    public enum EURA_DATA
    {
        //Empty 
    }
    public enum EURJ_DATA
    {
        //Empty 
    }
    public enum EURO_DATA
    {
        //Empty 
    }
    public enum EVDP_DATA
    {
        //Empty 
    }
    public enum EVNT_DATA
    {
        //Empty 
    }
    public enum EVVA_DATA
    {
        //Empty 
    }
    public enum FASC_DATA
    {
        //Empty 
    }
    public enum FASD_DATA
    {
        //Empty 
    }
    public enum FASL_DATA
    {
        //Empty 
    }
    public enum FASS_DATA
    {
        //Empty 
    }
    public enum FAST_DATA
    {
        //Empty 
    }
    public enum FCFP_DATA
    {
        //Empty 
    }
    public enum FCFS_DATA
    {
        //Empty 
    }
    public enum FCUS_DATA
    {
        //Empty 
    }
    public enum FMTX_DATA
    {
        //Empty 
    }
    public enum FNAR_DATA
    {
        //Empty 
    }
    public enum FRNA_DATA
    {
        //Empty 
    }
    public enum FSCR_DATA
    {
        //Empty 
    }
    public enum FUNC_DATA
    {
        //Empty 
    }
    public enum FURT_DATA
    {
        //Empty 
    }
    public enum GAUD_DATA
    {
        //Empty 
    }
    public enum GHIS_DATA
    {
        //Empty 
    }
    public enum GLED_DATA
    {
        //Empty 
    }
    public enum GLMS_DATA
    {
        //Empty 
    }
    public enum GLSB_DATA
    {
        //Empty 
    }
    public enum GREF_DATA
    {
        //Empty 
    }
    public enum GREP_DATA
    {
        //Empty 
    }
    public enum GRNQ_DATA
    {
        //Empty 
    }
    public enum HDES_DATA
    {
        //Empty 
    }
    public enum HGRN_DATA
    {
        //Empty 
    }
    public enum HINV_DATA
    {
        //Empty 
    }
    public enum HMOV_DATA
    {
        //Empty 
    }
    public enum HOLS_DATA
    {
        //Empty 
    }
    public enum HORD_DATA
    {
        //Empty 
    }
    public enum HSAF_DATA
    {
        //Empty 
    }
    public enum HTRM_DATA
    {
        //Empty 
    }
    public enum HVAR_DATA
    {
        //Empty 
    }
    public enum HVAT_DATA
    {
        //Empty 
    }
    public enum HVT1_DATA
    {
        //Empty 
    }
    public enum HVT2_DATA
    {
        //Empty 
    }
    public enum HVT3_DATA
    {
        //Empty 
    }
    public enum HWAL_DATA
    {
        //Empty 
    }
    public enum HWOP_DATA
    {
        //Empty 
    }
    public enum HWOR_DATA
    {
        //Empty 
    }
    public enum IDEP_DATA
    {
        //Empty 
    }
    public enum INAU_DATA
    {
        //Empty 
    }
    public enum INDE_DATA
    {
        //Empty 
    }
    public enum INFA_DATA
    {
        //Empty 
    }
    public enum INFB_DATA
    {
        //Empty 
    }
    public enum INFC_DATA
    {
        //Empty 
    }
    public enum INFD_DATA
    {
        //Empty 
    }
    public enum INFE_DATA
    {
        //Empty 
    }
    public enum INFF_DATA
    {
        //Empty 
    }
    public enum INFG_DATA
    {
        //Empty 
    }
    public enum IRAT_DATA
    {
        //Empty 
    }
    public enum ISSD_DATA
    {
        //Empty 
    }
    public enum ISSH_DATA
    {
        //Empty 
    }
    public enum ISSR_DATA
    {
        //Empty 
    }
    public enum JURN_DATA
    {
        //Empty 
    }
    public enum KANM_DATA
    {
        //Empty 
    }
    public enum KEYL_DATA
    {
        //Empty 
    }
    public enum KLOC_DATA
    {
        //Empty 
    }
    public enum KPAR_DATA
    {
        //Empty 
    }
    public enum KTYP_DATA
    {
        //Empty 
    }
    public enum LABL_DATA
    {
        //Empty 
    }
    public enum LABP_DATA
    {
        //Empty 
    }
    public enum LABR_DATA
    {
        //Empty 
    }
    public enum LABT_DATA
    {
        //Empty 
    }
    public enum LANG_DATA
    {
        //Empty 
    }
    public enum LDIM_DATA
    {
        //Empty 
    }
    public enum LEDG_DATA
    {
        //Empty 
    }
    public enum LKBX_DATA
    {
        //Empty 
    }
    public enum LLIN_DATA
    {
        //Empty 
    }
    public enum LLTR_DATA
    {
        //Empty 
    }
    public enum LNAL_DATA
    {
        //Empty 
    }
    public enum LNKS_DATA
    {
        //Empty 
    }
    public enum LNOT_DATA
    {
        //Empty 
    }
    public enum LNPK_DATA
    {
        //Empty 
    }
    public enum LOAD_DATA
    {
        //Empty 
    }
    public enum LRES_DATA
    {
        //Empty 
    }
    public enum LTAT_DATA
    {
        //Empty 
    }
    public enum MACC_DATA
    {
        //Empty 
    }
    public enum MACH_DATA
    {
        //Empty 
    }
    public enum MACL_DATA
    {
        //Empty 
    }
    public enum MACX_DATA
    {
        //Empty 
    }
    public enum MATX_DATA
    {
        //Empty 
    }
    public enum MAXT_DATA
    {
        //Empty 
    }
    public enum MBM1_DATA
    {
        //Empty 
    }
    public enum MBM2_DATA
    {
        //Empty 
    }
    public enum MBM3_DATA
    {
        //Empty 
    }
    public enum MBM4_DATA
    {
        //Empty 
    }
    public enum MBM5_DATA
    {
        //Empty 
    }
    public enum MBM6_DATA
    {
        //Empty 
    }
    public enum MBM7_DATA
    {
        //Empty 
    }
    public enum MBM8_DATA
    {
        //Empty 
    }
    public enum MBM9_DATA
    {
        //Empty 
    }
    public enum MCLT_DATA
    {
        //Empty 
    }
    public enum MEMS_DATA
    {
        //Empty 
    }
    public enum MMD_ME_SCON_STK
    {
        //Empty 
    }
    public enum MOBL_DATA
    {
        //Empty 
    }
    public enum MODD_DATA{
        //Empty 
    }
    public enum MODH_DATA{
        //Empty 
    }
    public enum MODL_DATA{
        //Empty 
    }
    public enum MODS_DATA{
        //Empty 
    }
    public enum MODX_DATA{
        //Empty 
    }
    public enum MOPA_DATA{
        //Empty 
    }
    public enum MOPD_DATA{
        //Empty 
    }
    public enum MOPN_DATA{
        //Empty 
    }
    public enum MOPV_DATA{
        //Empty 
    }
    public enum MPRT_DATA{
        //Empty 
    }
    public enum MRES_DATA{
        //Empty 
    }
    public enum MRPP_DATA{
        //Empty 
    }
    public enum MRPX_DATA{
        //Empty 
    }
    public enum MRSP_DATA{
        //Empty 
    }
    public enum MRWK_DATA{
        //Empty 
    }
    public enum MSGP_DATA{
        //Empty 
    }
    public enum NAME_DATA{
        //Empty 
    }
    public enum NAPS_DATA{
        //Empty 
    }
    public enum NARX_DATA{
        //Empty 
    }
    public enum NARY_DATA{
        //Empty 
    }
    public enum NEXT_DATA{
        //Empty 
    }
    public enum NSEG_DATA{
        //Empty 
    }
    public enum OCAL_DATA{
        //Empty 
    }
    public enum OCAM_DATA{
        //Empty 
    }
    public enum ODES_DATA{
        //Empty 
    }
    public enum OFFA_DATA{
        //Empty 
    }
    public enum OIXT_DATA{
        //Empty 
    }
    public enum OLSL_DATA{
        //Empty 
    }
    public enum OPHI_DATA{
        //Empty 
    }
    public enum OPRS_DATA{
        //Empty 
    }
    public enum ORDA_DATA{
        //Empty 
    }
    public enum OTMP_DATA{
        //Empty 
    }
    public enum OVAL_DATA{
        //Empty 
    }
    public enum OWNR_DATA{
        //Empty 
    }
    public enum PACC_DATA{
        //Empty 
    }
    public enum PACK_DATA{
        //Empty 
    }
    public enum PAKS_DATA{
        //Empty 
    }
    public enum PAKW_DATA{
        //Empty 
    }
    public enum PALC_DATA{
        //Empty 
    }
    public enum PALS_DATA{
        //Empty 
    }
    public enum PBMS_DATA{
        //Empty 
    }
    public enum PCAD_DATA{
        //Empty 
    }
    public enum PCBK_DATA{
        //Empty 
    }
    public enum PCEN_DATA{
        //Empty 
    }
    public enum PCON_DATA{
        //Empty 
    }
    public enum PCRM_DATA{
        //Empty 
    }
    public enum PCSA_DATA{
        //Empty 
    }
    public enum PCSD_DATA{
        //Empty 
    }
    public enum PCVA_DATA{
        //Empty 
    }
    public enum PDRW_DATA{
        //Empty 
    }
    public enum PDUN_DATA{
        //Empty 
    }
    public enum PEAN_DATA{
        //Empty 
    }
    public enum PERR_DATA{
        //Empty 
    }
    public enum PEST_DATA{
        //Empty 
    }
    public enum PFLT_DATA{
        //Empty 
    }
    public enum PGDQ_DATA{
        //Empty 
    }
    public enum PHSA_DATA{
        //Empty 
    }
    public enum PHSN_DATA{
        //Empty 
    }
    public enum PICK_DATA{
        //Empty 
    }
    public enum PIDQ_DATA{
        //Empty 
    }
    public enum PIEC_DATA{
        //Empty 
    }
    public enum PISD_DATA{
        //Empty 
    }
    public enum PJRN_DATA{
        //Empty 
    }
    public enum PLCB_DATA{
        //Empty 
    }
    public enum PLCD_DATA{
        //Empty 
    }
    public enum PLCM_DATA{
        //Empty 
    }
    public enum PLCN_DATA{
        //Empty 
    }
    public enum PLDC_DATA{
        //Empty 
    }
    public enum PLDR_DATA{
        //Empty 
    }
    public enum PLED_DATA{
        //Empty 
    }
    public enum PLGD_DATA{
        //Empty 
    }
    public enum PLGS_DATA{
        //Empty 
    }
    public enum PLIH_DATA{
        //Empty 
    }
    public enum PLIL_DATA{
        //Empty 
    }
    public enum PLIS_DATA{
        //Empty 
    }
    public enum PLIT_DATA{
        //Empty 
    }
    public enum PLNB_DATA{
        //Empty 
    }
    public enum PLNK_DATA{
        //Empty 
    }
    public enum PLPA_DATA{
        //Empty 
    }
    public enum PLPB_DATA{
        //Empty 
    }
    public enum PLRB_DATA{
        //Empty 
    }
    public enum PLRD_DATA{
        //Empty 
    }
    public enum PLRN_DATA{
        //Empty 
    }
    public enum PLSB_DATA{
        //Empty 
    }
    public enum PLSD_DATA{
        //Empty 
    }
    public enum PLSN_DATA{
        //Empty 
    }
    public enum PLTD_DATA{
        //Empty 
    }
    public enum PMOL_DATA{
        //Empty 
    }
    public enum PMTM_DATA{
        //Empty 
    }
    public enum POPR_DATA{
        //Empty 
    }
    public enum POSS_DATA{
        //Empty 
    }
    public enum PPMR_DATA{
        //Empty 
    }
    public enum PPMW_DATA{
        //Empty 
    }
    public enum PREF_DATA{
        //Empty 
    }
    public enum PROC_DATA{
        //Empty 
    }
    public enum PROD_DATA{
        //Empty 
    }
    public enum PSPB_DATA{
        //Empty 
    }
    public enum PSPT_DATA{
        //Empty 
    }
    public enum PSPX_DATA{
        //Empty 
    }
    public enum PTAG_DATA{
        //Empty 
    }
    public enum PTDC_DATA{
        //Empty 
    }
    public enum PTDH_DATA{
        //Empty 
    }
    public enum PTEX_DATA{
        //Empty 
    }
    public enum PTGT_DATA{
        //Empty 
    }
    public enum PTRN_DATA{
        //Empty 
    }
    public enum PTYP_DATA{
        //Empty 
    }
    public enum PUCD_DATA{
        //Empty 
    }
    public enum PUDW_DATA{
        //Empty 
    }
    public enum PWID_DATA{
        //Empty 
    }
    public enum QADX_DATA{
        //Empty 
    }
    public enum QATT_DATA{
        //Empty 
    }
    public enum QAUD_DATA{
        //Empty 
    }
    public enum QCAA_DATA{
        //Empty 
    }
    public enum QCAC_DATA{
        //Empty 
    }
    public enum QCAL_DATA{
        //Empty 
    }
    public enum QCAN_DATA{
        //Empty 
    }
    public enum QCAR_DATA{
        //Empty 
    }
    public enum QCAT_DATA{
        //Empty 
    }
    public enum QCCA_DATA{
        //Empty 
    }
    public enum QCCM_DATA{
        //Empty 
    }
    public enum QCCN_DATA{
        //Empty 
    }
    public enum QCCR_DATA{
        //Empty 
    }
    public enum QCCS_DATA{
        //Empty 
    }
    public enum QCDD_DATA{
        //Empty 
    }
    public enum QCDF_DATA{
        //Empty 
    }
    public enum QCDG_DATA{
        //Empty 
    }
    public enum QCDP_DATA{
        //Empty 
    }
    public enum QCFC_DATA{
        //Empty 
    }
    public enum QCFD_DATA{
        //Empty 
    }
    public enum QCFM_DATA{
        //Empty 
    }
    public enum QCFN_DATA{
        //Empty 
    }
    public enum QCFT_DATA{
        //Empty 
    }
    public enum QCHT_DATA{
        //Empty 
    }
    public enum QCLN_DATA{
        //Empty 
    }
    public enum QCLT_DATA{
        //Empty 
    }
    public enum QCMC_DATA{
        //Empty 
    }
    public enum QCNC_DATA{
        //Empty 
    }
    public enum QCNF_DATA{
        //Empty 
    }
    public enum QCPN_DATA{
        //Empty 
    }
    public enum QCPR_DATA{
        //Empty 
    }
    public enum QCPT_DATA{
        //Empty 
    }
    public enum QCRC_DATA{
        //Empty 
    }
    public enum QCRK_DATA{
        //Empty 
    }
    public enum QCRN_DATA{
        //Empty 
    }
    public enum QCRR_DATA{
        //Empty 
    }
    public enum QCRU_DATA{
        //Empty 
    }
    public enum QCSA_DATA{
        //Empty 
    }
    public enum QCSP_DATA{
        //Empty 
    }
    public enum QCSR_DATA{
        //Empty 
    }
    public enum QCVR_DATA{
        //Empty 
    }
    public enum QGRD_DATA{
        //Empty 
    }
    public enum QHDR_DATA{
        //Empty 
    }
    public enum QIDF_DATA{
        //Empty 
    }
    public enum QLIB_DATA{
        //Empty 
    }
    public enum QNLC_DATA{
        //Empty 
    }
    public enum QNLN_DATA{
        //Empty 
    }
    public enum QNLR_DATA{
        //Empty 
    }
    public enum QNLS_DATA{
        //Empty 
    }
    public enum QOPS_DATA{
        //Empty 
    }
    public enum QPAR_DATA{
        //Empty 
    }
    public enum QREF_DATA{
        //Empty 
    }
    public enum QSER_DATA{
        //Empty 
    }
    public enum QSPA_DATA{
        //Empty 
    }
    public enum QSPG_DATA{
        //Empty 
    }
    public enum QSPT_DATA{
        //Empty 
    }
    public enum QTFB_DATA{
        //Empty 
    }
    public enum QTMP_DATA{
        //Empty 
    }
    public enum QTMR_DATA{
        //Empty 
    }
    public enum QTST_DATA{
        //Empty 
    }
    public enum QTTD_DATA{
        //Empty 
    }
    public enum QUSE_DATA{
        //Empty 
    }
    public enum RDAY_DATA{
        //Empty 
    }
    public enum REPO_DATA{
        //Empty 
    }
    public enum RETN_DATA{
        //Empty 
    }
    public enum RGAX_DATA{
        //Empty 
    }
    public enum RHCR_DATA{
        //Empty 
    }
    public enum RSOP_DATA
    {
        //Empty 
    }
    public enum SABK_DATA
    {
        //Empty 
    }
    public enum SALS_DATA
    {
        //Empty 
    }
    public enum SAMP_DATA
    {
        //Empty 
    }
    public enum SANL_DATA
    {
        //Empty 
    }
    public enum SAOI_DATA
    {
        //Empty 
    }
    public enum SATD_DATA
    {
        //Empty 
    }
    public enum SATP_DATA
    {
        //Empty 
    }
    public enum SBUD_DATA
    {
        //Empty 
    }
    public enum SCBT_DATA
    {
        //Empty 
    }
    public enum SCHG_DATA
    {
        //Empty 
    }
    public enum SCRD_DATA
    {
        //Empty 
    }
    public enum SCRP_DATA
    {
        //Empty 
    }
    public enum SCSP_DATA
    {
        //Empty 
    }
    public enum SDET_DATA
    {
        //Empty 
    }
    public enum SDOC_DATA
    {
        //Empty 
    }
    public enum SERB_DATA
    {
        //Empty 
    }
    public enum SERL_DATA
    {
        //Empty 
    }
    public enum SGRP_DATA
    {
        //Empty 
    }
    public enum SHIF_DATA
    {
        //Empty 
    }
    public enum SHOT_DATA
    {
        //Empty 
    }
    public enum SLED_DATA
    {
        //Empty 
    }
    public enum SLNK_DATA
    {
        //Empty 
    }
    public enum SPAR_DATA
    {
        //Empty 
    }
    public enum SPEC_DATA
    {
        //Empty 
    }
    public enum SPMC_DATA
    {
        //Empty 
    }
    public enum SPNA_DATA
    {
        //Empty 
    }
    public enum SPRD_DATA
    {
        //Empty 
    }
    public enum SQPR_DATA
    {
        //Empty 
    }
    public enum SREF_DATA
    {
        //Empty 
    }
    public enum SREV_DATA
    {
        //Empty 
    }
    public enum SSTK_DATA
    {
        //Empty 
    }
    public enum STAT_DATA
    {
        //Empty 
    }
    public enum STKP_DATA
    {
        //Empty 
    }
    public enum STPA_DATA
    {
        //Empty 
    }
    public enum STUB_DATA
    {
        //Empty 
    }
    public enum SUBH_DATA
    {
        //Empty 
    }
    public enum SUBL_DATA
    {
        //Empty 
    }
    public enum SUGP_DATA
    {
        //Empty 
    }
    public enum SUMD_DATA
    {
        //Empty 
    }
    public enum SVAH_DATA
    {
        //Empty 
    }
    public enum SVAN_DATA
    {
        //Empty 
    }
    public enum SVAR_DATA
    {
        //Empty 
    }
    public enum SVAT_DATA
    {
        //Empty 
    }
    public enum SVOP_DATA
    {
        //Empty 
    }
    public enum SVPM_DATA
    {
        //Empty 
    }
    public enum SVPR_DATA
    {
        //Empty 
    }
    public enum SYSL_DATA
    {
        //Empty 
    }
    public enum TABT_DATA
    {
        //Empty 
    }
    public enum TACL_DATA
    {
        //Empty 
    }
    public enum TACM_DATA
    {
        //Empty 
    }
    public enum TANK_DATA
    {
        //Empty 
    }
    public enum TDIP_DATA
    {
        //Empty 
    }
    public enum TEMP_BOM
    {
        //Empty 
    }
    public enum TERM_DATA
    {
        //Empty 
    }
    public enum TEST_DATA
    {
        //Empty 
    }
    public enum TOOL_DATA
    {
        //Empty 
    }
    public enum TPOS_DATA
    {
        //Empty 
    }
    public enum TRAL_DATA
    {
        //Empty 
    }
    public enum TRAM_DATA
    {
        //Empty 
    }
    public enum TRAN_DATA
    {
        //Empty 
    }
    public enum TRAP_DATA
    {
        //Empty 
    }
    public enum TRAR_DATA
    {
        //Empty 
    }
    public enum TRAS_DATA
    {
        //Empty 
    }
    public enum TRIG_DATA
    {
        //Empty 
    }
    public enum USRB_DATA
    {
        //Empty 
    }
    public enum USRC_DATA
    {
        //Empty 
    }
    public enum VALD_DATA
    {
        //Empty 
    }
    public enum VALH_DATA
    {
        //Empty 
    }
    public enum VALS_DATA
    {
        //Empty 
    }
    public enum VALX_DATA
    {
        //Empty 
    }
    public enum VERC_DATA
    {
        //Empty 
    }
    public enum WART_DATA
    {
        //Empty 
    }
    public enum WAST_DATA
    {
        //Empty 
    }
    public enum WCHT_DATA
    {
        //Empty 
    }
    public enum WCSP_DATA
    {
        //Empty 
    }
    public enum WGSP_DATA
    {
        //Empty 
    }
    public enum WHCT_DATA
    {
        //Empty 
    }
    public enum WHOH_DATA
    {
        //Empty 
    }
    public enum WHOL_DATA
    {
        //Empty 
    }
    public enum WIPD_DATA
    {
        //Empty 
    }
    public enum WIPK_DATA
    {
        //Empty 
    }
    public enum WKIT_DATA
    {
        //Empty 
    }
    public enum WLAT_DATA
    {
        //Empty 
    }
    public enum WORH_DATA
    {
        //Empty 
    }
    public enum WORP_DATA
    {
        //Empty 
    }
    public enum WOSC_DATA
    {
        //Empty 
    }
    public enum WPRM_DATA
    {
        //Empty 
    }
    public enum WRAH_DATA
    {
        //Empty 
    }
    public enum WRAL_DATA
    {
        //Empty 
    }
    public enum WSTK_DATA
    {
        //Empty 
    }
    public enum WTCA_DATA
    {
        //Empty 
    }
    public enum WTCB_DATA
    {
        //Empty 
    }
    public enum WTCH_DATA
    {
        //Empty 
    }
    public enum WTCL_DATA
    {
        //Empty 
    }
    public enum WTCP_DATA
    {
        //Empty 
    }
    public enum WTDR_DATA
    {
        //Empty 
    }
    public enum WTLR_DATA
    {
        //Empty 
    }
    public enum WTMC_DATA
    {
        //Empty 
    }
    public enum WTPA_DATA
    {
        //Empty 
    }
    public enum WTRR_DATA
    {
        //Empty 
    }
    public enum WTSR_DATA
    {
        //Empty 
    }
    public enum WTST_DATA
    {
        //Empty 
    }
    public enum WTSU_DATA
    {
        //Empty 
    }
    public enum WTVE_DATA
    {
        //Empty 
    }
    public enum WTVS_DATA
    {
        //Empty 
    }
    public enum WTVW_DATA
    {
        //Empty 
    }
    public enum WTWC_DATA
    {
        //Empty 
    }
    public enum XCST_DATA
    {
        //Empty 
    }
    public enum XFER_DATA
    {
        //Empty 
    }
    public enum ZIPC_DATA
    {
        //Empty 
    }
}