import { Photo } from "./photo";

export interface Customer {
    id: string;
    userName: string;
    lookingFor: string;
    mainPhotoUrl: string;
    photos: Photo[];
}
