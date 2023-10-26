export interface SpecialOffer {
  id?: string
  photoId?: string
  title: string;
  description: string;
  backgroundColor: string;
  imageBase64: string;
  textColor: string
  published: boolean;
}

export interface PublishUnpublishRequest {
  OfferId: string;
}
