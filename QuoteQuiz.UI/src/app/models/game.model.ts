export enum GameMode {
  Binary = 1,
  MultipleAnswer = 2
}

export interface AuthorOption {
  id: number;
  name: string;
}

export interface StartGameResponse {
  sessionId: number;
  mode: GameMode;
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
