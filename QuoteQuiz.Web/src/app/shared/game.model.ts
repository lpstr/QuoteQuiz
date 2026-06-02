export type GameMode = 'Binary' | 'MultipleChoice';

export interface StartGameRequest {
  userId: number;
  mode: GameMode;
}

export interface StartGameResponse {
  sessionId: number;
  mode: GameMode;
}

export interface AuthorOption {
  id: number;
  name: string;
}

export interface NextQuestionResponse {
  sessionId: number;
  quoteId: number;
  quoteText: string;
  mode: GameMode;
  suggestedAuthorId?: number;
  suggestedAuthorName?: string;
  options?: AuthorOption[];
}

export interface SubmitAnswerRequest {
  sessionId: number;
  quoteId: number;
  mode: GameMode;
  selectedAuthorId?: number;
  answerYesNo?: boolean;
}

export interface SubmitAnswerResponse {
  isCorrect: boolean;
  correctAuthor: string;
  quoteText: string;
}
