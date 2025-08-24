import {
  useController,
  type FieldValues,
  type UseControllerProps,
} from "react-hook-form";
import type { LocationIQSuggestion } from "../../../lib/types";
import { Box, debounce, List, ListItemButton, TextField, Typography } from "@mui/material";
import { useEffect, useMemo, useState } from "react";
import axios from "axios";

type Props<T extends FieldValues> = {
    label: string
} & UseControllerProps<T> 

export default function LocationInput<T extends FieldValues>(props: Props<T>) {
  const { field, fieldState } = useController<T>({ ...props });
  const [loading, setLoading] = useState(false);
  const [suggestions, setSuggestions] = useState<LocationIQSuggestion[]>([]);
  const [inputValue, setInputValue] = useState(field.value || "");
  
  useEffect(() => {
    if(field.value && typeof field.value === "object") {
        setInputValue(field.value.venue || "");
    } else {
        setInputValue(field.value || "");
    }
  }, [field.value]);

  const locationUrl = "https://api.locationiq.com/v1/autocomplete?key=pk.1aa54cb78f6b83db8a3e81b33e1aa756&q=repunta&limit=5&dedupe=1&"
  
  const fetchSuggestions = useMemo(() => 
    debounce(async (query: string) => {
        if(!query || query.length === 0) {
            setSuggestions([]);
            return;
        }

        setLoading(true);

        try {
            const response = await axios.get<LocationIQSuggestion[]>(`${locationUrl}q=${query}`);
            setSuggestions(response.data);
        } catch (error) {
            console.log(error);
        } finally {
            setLoading(false);
        }

  }, 500), [locationUrl]);
  
  const handelChange = async (value: string) => {
    field.onChange(value);
    await fetchSuggestions(value);
  }

  const handleSelect = (location: LocationIQSuggestion) => {
    const city = location.address?.city || location.address?.town || location.address?.name || "";
    const venue = location.display_name;
    const latitud = Number(location.lat);
    const longitud = Number(location.lon);
    setInputValue(venue);
    field.onChange({city, venue, latitud, longitud});
    setSuggestions([]);
  }
  return (
    <Box>
        <TextField
        {...props}
        value={inputValue}
        onChange={e => handelChange(e.target.value)}
        fullWidth
        variant="outlined"
        error={!!fieldState.error}
        helperText={fieldState.error?.message}
        />
        {loading && <Typography>Loading...</Typography>}
        {suggestions.length > 0 && (
            <List sx={{border: 1}}>
                {suggestions.map(suggestion => (
                    <ListItemButton divider key={suggestion.place_id} onClick={() => handleSelect(suggestion)}>
                        {suggestion.display_name}
                    </ListItemButton>
                ))}
            </List>
        )}
    </Box>
  );
}