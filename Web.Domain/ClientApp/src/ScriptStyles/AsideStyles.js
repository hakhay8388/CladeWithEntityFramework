import GlobalStyles from "./GlobalStyles";

const AsideStyles = Theme => ({
  ...GlobalStyles(Theme),

  notificationTitle: {
    borderTopLeftRadius: "0.25rem",
    borderTopRightRadius: "0.25rem",
    borderLeft: "4px solid #c8ced3",
    color: "#73818f !important",
    fontWeight: "bold !important",
    textTransform: "uppercase",
    textAlign: "center",
    backgroundColor: "#f0f3f5",
    position: "relative",
    display: "block",
    padding: "0.75rem 1.25rem",
    marginBottom: "-1px",
    border: "1px solid rgba(0,0,0,0.125)",
    fontSize: "80%"
  },
  fragmentStyle: {
    width: "auto",
    overflow: "auto",
    height: "100%"
  },
  listGroupItem: {
    position: "relative",
    display: "block",
    padding: "0.75rem 1.25rem",
    marginBottom: "-1px",
    border: "1px solid rgba(0,0,0,0.125)"
  },
  listGroupItemAccentInfo: {
    borderLeft: "4px solid #63c2de",
  },
  avatar: {
    position: "relative",
    display: "inline-block",
    width: "36px",
    height: "36px",
  },
  floatLeft: {
    float: "left"
  },
  floatRight: {
    float: "right"
  },
  imgAvatar: {
    maxWidth: "100%",
    height: "auto",
    borderRadius: "50em",
  },
  textMuted: {
    color: "#73818f !important"
  },
  mr3:{
    marginRight: "1rem !important"
  }
})

export default AsideStyles
