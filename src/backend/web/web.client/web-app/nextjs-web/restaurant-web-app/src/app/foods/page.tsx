import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import RestClient from '@/api/RestApiClient';
import { IFoodDto } from '@/api/dtos/FoodDto';
import FoodsList from './FoodsList';


const FoodsListContainer = () => {
  const [foods, setFoods] = useState<IFoodDto[]>([]);
  const router = useRouter();

  useEffect(() => {
    RestClient.getFoods("token").then(foods => {
      foods.forEach(f => {
        f.pictures.forEach(p => {
          p.filePath = process.env.REACT_APP_BACKEND_URL + '/catalog/' + p.filePath;
        });
      });
      setFoods(foods);
    });
  }, []);

  return <FoodsList foods={foods} router={router} />;
};

export default FoodsListContainer;
