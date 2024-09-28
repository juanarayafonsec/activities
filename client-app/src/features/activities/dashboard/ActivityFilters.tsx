import Calendar from "react-calendar";
import { Header, Menu, MenuItem } from "semantic-ui-react";

export default function ActivityFilters(){
    return(
        <>
        <Menu vertical size="large" style={{width:"100%", marginTop:25}}>
            <Header icon="filter" attached="top" color="teal"/>
            <MenuItem content="All Activities"/>
            <MenuItem content="I am going"/>
            <MenuItem content="I am hosting"/>
        </Menu>
        <Header/>
        <Calendar/>
        </>
        
    )
}