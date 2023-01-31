import DefaultTheme from "../Themes/DefaultTheme";
import {alpha} from "@material-ui/core/styles";

const UsersCommonHeaderStyles = (theme) => ({
  customBadge: {
    backgroundColor: DefaultTheme.palette.secondary.alternativeLight,
    color: "white",
  },
  iconButton: {
    padding: 10,
    marginLeft: 10
  },
  iconButtonSmall: {
    marginLeft: 5,
    marginRight: "-5px"
  }
  ,
  root: {
    padding: "2px 4px",
    display: "flex",
    alignItems: "center",
    "@media screen and (max-width: 360px)": {
      width: 170,
    },
    "@media screen and (max-width: 400px) and (min-width: 360px)": {
      width: 175,
    },
    "@media screen and (max-width: 500px) and (min-width: 400px)": {
      width: 235,
    },
    "@media screen and (max-width: 600px) and (min-width: 500px)": {
      width: 320,
    },
    [theme.breakpoints.up("sm")]: {
      width: 280,
    },
    [theme.breakpoints.up("md")]: {
      width: 400,
    },
  },
  textArea: {
    marginLeft: "25px",
    [theme.breakpoints.down("sm")]: {
      marginLeft: "-5px",
    },
  },
  searchArea: {
    marginBlockStart: "5px",
  },
  headerButtons: {
    margin: "auto",
    textAlign: "center",
  },
  input: {
    marginLeft: theme.spacing(1),
    flex: 1,
    fontFamily: "Montserrat",
    fontWeight: "bold",
    [theme.breakpoints.down("xs")]: {
      fontSize: "11px",
    },
    [theme.breakpoints.up("sm")]: {
      fontSize: "11px",
    },
    [theme.breakpoints.up("md")]: {
      fontSize: "12px",
    },
    [theme.breakpoints.up("lg")]: {
      fontSize: "15px",
    },
  },
  headerButtonHover: {
    color: DefaultTheme.palette.default.fifthAlternative,
    "&:hover": {
      color: DefaultTheme.palette.primary.main
    }
  },
  menuStyles: {
    left: "180px",
    width: "160px"
  },
  profileImage: {
    borderBottom: "0.1em solid " + DefaultTheme.palette.default.fourthAlternative,
    padding: "20px",
  },
  grow: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    display: 'block',
    [theme.breakpoints.down('sm')]: {
      display: 'none',
    },
  },
  search: {
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: alpha(theme.palette.common.white, 0.15),
    '&:hover': {
      backgroundColor: alpha(theme.palette.common.white, 0.25),
    },
    marginRight: theme.spacing(2),
    marginLeft: 0,
    width: '100%',
    [theme.breakpoints.up('sm')]: {
      width: 'auto',
    },
  },
  searchIcon: {
    padding: theme.spacing(0, 2),
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  inputRoot: {
    color: 'inherit',
  },
  inputInput: {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('md')]: {
      width: '20ch',
    },
  },
  sectionDesktop: {
    display: 'flex',
    [theme.breakpoints.down('xs')]: {
      display: 'none',
    },
  },
  sectionMobile: {
    display: 'flex',
    [theme.breakpoints.up('sm')]: {
      display: 'none',
    },
  },
  languageIcon: {
    minWidth: "30px"
  },
  iconWithoutHover: {
    backgroundColor: "transparent"
  },
  appBarStyle: {
    backgroundColor: DefaultTheme.palette.secondary.contrastText,
    boxShadow: "0px 0.01rem"
  },
  anchorTopRight: {
    top: "10px",
    right: "5px"
  },
  anchorTopRightInMobile: {
    top: "5px",
    right: "5px"
  }
});

export default UsersCommonHeaderStyles
