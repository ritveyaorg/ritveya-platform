export interface ContactSubmissionCreate {
  name: string;
  email?: string;
  phone?: string;
  message: string;
  sourcePage?: string;
}

