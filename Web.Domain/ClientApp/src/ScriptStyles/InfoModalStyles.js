
import GlobalStyles from "./GlobalStyles"

const InfoModalStyles = Theme => ({
  ...GlobalStyles(Theme),
  dialog: {
    position: 'absolute',
    top: 50,
    margin : '0px !important'
  }
});


export default InfoModalStyles
