import { IFoodDto } from '@/api/dtos/FoodDto';
import * as React from 'react';
import FoodItem from './FoodItem';
import { AppRouterInstance } from 'next/dist/shared/lib/app-router-context.shared-runtime';
import { NextRouter } from 'next/router';

interface Props {
  foods: IFoodDto[];
  router: NextRouter
}

const FoodsList: React.FC<Props> = props => {
  return (
    <div style={{ flex: 1 }}>
      {props.foods.map(item => {
        return <FoodItem {...props} key={item.id} item={item} />;
      })}
    </div>
  );
};

export default FoodsList;