
import GlobalStyles from "./GlobalStyles"

const BatchJobEditModalStyles = Theme => ({

  ...GlobalStyles(Theme),

  contentArea: {
    [Theme.breakpoints.up('sm')]: {
      width: "800px",
    },
    [Theme.breakpoints.down('sm')]: {
      width: "500px",
    },
    [Theme.breakpoints.down('xs')]: {
      width: "100%",
    },
  }
  ,
  textArea: {
    width: "100%",
    padding: "5px 0px 0px 0px"
  }
  ,
  box: {
    padding: "0px 8px 8px 8px"
  }
  ,
  stepLabel: {
    [Theme.breakpoints.up('sm')]: {
      fontSize: "14px"
    },
    [Theme.breakpoints.down('xs')]: {
      fontSize: "11px"
    },
  },
  iconContainer: {
    transform: "scale(0.7)",
  },
  buttonsGrid: {
    [Theme.breakpoints.down("xs")]: {
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
    },
  },
  DialogModal: {
    minHeight: "500px"
  }
});


export default BatchJobEditModalStyles
