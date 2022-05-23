import axios, { AxiosInstance } from 'axios';
import { ErrorDto } from './dtos/ErrorDto';
import { IFoodDto } from './dtos/FoodDto';

class RestApiClient {
	private accessToken: string;
	
	private get AccessToken() : string {
		return this.accessToken;
	}

	private set AccessToken(accessToken: string) {
		this.accessToken = accessToken;
	}

	private backend: AxiosInstance = this.createAxios();

    private createAxios(): AxiosInstance {
		const instance = axios.create({
			baseURL: process.env.REACT_APP_BACKEND_URL,
			timeout: 5000,
			headers: { "Content-Type": "text/json" }
		});

		instance.interceptors.request.use(config => {
			if (config && config.headers) {
				config.headers.Authorization = `Bearer ${this.AccessToken}`;
			}
			return config;
		}, error => Promise.reject(error));

		instance.interceptors.response.use(
			response => response,
			error => {
				return Promise.reject(error.response ? ErrorDto.fromJson(error.response.data) : new ErrorDto(-1, "no response"));
			});

		return instance;
	}

	public async getFoods(token: string): Promise<IFoodDto[]> {
		this.AccessToken = token;

		const response = await this.backend.get<IFoodDto[]>('/menu/api/v1/foods');
		return response.data;
	}

	public async getFood(token: string, foodId: string) {
		this.AccessToken = token;
		const response = await this.backend.get<IFoodDto>(`/menu/api/v1/foods/${foodId}`);
		return response.data;
	}
}

const RestClient = new RestApiClient();
export default RestClient;